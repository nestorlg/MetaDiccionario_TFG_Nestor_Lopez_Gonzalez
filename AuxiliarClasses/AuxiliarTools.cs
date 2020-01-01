using System;
using System.Data.OleDb;
using System.Globalization;

namespace MetaDiccionario
{
	public class AuxiliarTools
	{
		public static String Word_converter(String word)
		{
			int length = word.Length;
			int i;
			String result = "";
			for (i = 0; i < length; i++)
			{
				char c = word[i];
				switch (c)
				{
					case 'a':
					case 'á':
					case 'ä':
					case 'à':
					case 'â':
					case 'ã':
					case 'A':
					case 'Á':
					case 'Ä':
					case 'À':
					case 'Â':
					case 'Ã':
						result += "[a,á,ä,à,â,ã]";
						break;
					case 'e':
					case 'é':
					case 'ë':
					case 'è':
					case 'ê':
					case 'E':
					case 'É':
					case 'Ë':
					case 'È':
					case 'Ê':
						result += "[e,é,ë,è,ê]";
						break;
					case 'i':
					case 'í':
					case 'ï':
					case 'ì':
					case 'î':
					case 'I':
					case 'Í':
					case 'Ï':
					case 'Ì':
					case 'Î':
						result += "[i,í,ï,ì,î]";
						break;
					case 'o':
					case 'ó':
					case 'ö':
					case 'ò':
					case 'ô':
					case 'õ':
					case 'O':
					case 'Ó':
					case 'Ö':
					case 'Ò':
					case 'Ô':
					case 'Õ':
						result += "[o,ó,ö,ò,ô,õ]";
						break;
					case 'u':
					case 'ú':
					case 'ü':
					case 'ù':
					case 'û':
					case 'U':
					case 'Ú':
					case 'Ü':
					case 'Ù':
					case 'Û':
						result += "[u,ú,ü,ù,û]";
						break;
					default:
						result += c;
						break;
				}
			}
			return result;
		}

		public static Tree FindFatherLocations(String localizacion,int id_localizacion,int id_loc_padre,int nivel,Tree tree,OleDbConnection connection)
		{
			if (id_loc_padre != 0)
			{
				OleDbCommand command = new OleDbCommand("SELECT localizacion, id_localizacion, id_loc_padre, nivel FROM Localizaciones WHERE id_localizacion=" + id_loc_padre + ";", connection);
				OleDbDataReader dr;
				try
				{
					dr = command.ExecuteReader();
					if (dr.Read())
					{
						String new_localizacion = dr.GetString(0);
						int new_id_localizacion = dr.GetInt32(1);
						int new_id_loc_padre = dr.GetInt32(2);
						int new_nivel = dr.GetInt32(3);
						tree = FindFatherLocations(new_localizacion, new_id_localizacion, new_id_loc_padre, new_nivel, tree, connection);
						tree.GetSon(new_id_localizacion).AddSon(id_localizacion, localizacion, id_loc_padre, nivel);
					}
				}
				catch
				{

				}
			}
			else
			{
				tree.AddSonToRoot(id_localizacion, localizacion, id_loc_padre, nivel);
			}
			return tree;
		}

		public static Tree FindFatherThematicFields(String campo_tematico, int id_campo_tematico, int id_campo_padre, int nivel, Tree tree, OleDbConnection connection)
		{
			if (id_campo_padre != 0)
			{
				OleDbCommand command = new OleDbCommand("SELECT campo_tematico, id_campo_tematico, id_campo_padre, nivel FROM CamposTematicos WHERE id_campo_tematico=" + id_campo_padre + ";", connection);
				OleDbDataReader dr;
				try
				{
					dr = command.ExecuteReader();
					if (dr.Read())
					{
						String new_campo_tematico = dr.GetString(0);
						int new_id_campo_tematico = dr.GetInt32(1);
						int new_id_campo_padre = dr.GetInt32(2);
						int new_nivel = dr.GetInt32(3);
						tree = FindFatherThematicFields(new_campo_tematico, new_id_campo_tematico, new_id_campo_padre, new_nivel, tree, connection);
						tree.GetSon(new_id_campo_tematico).AddSon(id_campo_tematico, campo_tematico, id_campo_padre, nivel);
					}
				}
				catch
				{

				}
			}
			else
			{
				tree.AddSonToRoot(id_campo_tematico, campo_tematico, id_campo_padre, nivel);
			}
			return tree;
		}

		public static String Busqueda_Diccionario (OleDbConnection connection, String word)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE lema LIKE '%" + AuxiliarTools.Word_converter(word) + "%' ORDER BY lema,etimología;", connection);
			connection.Open();
			String contenido_lemas_encontrados = "";
			try
			{
				OleDbDataReader dr = command.ExecuteReader();
				contenido_lemas_encontrados = "";
				contenido_lemas_encontrados += "<ul style = \"font-size:large;\">";
				int id_lema = 0;
				String lema = "";
				String etimologia;
				int count = 0;
				int id_acepcion = 0;
				while (dr.Read())
				{
					id_lema = dr.GetInt32(0);
					lema = dr.GetString(1).ToString();
					etimologia = dr.GetString(2).ToString();
					OleDbCommand command_acepcion = new OleDbCommand("SELECT id_acepcion FROM Acepciones WHERE (NumAcepcion=1 AND id_lema=" + id_lema + ");", connection);
					try
					{
						OleDbDataReader dr_acepcion = command_acepcion.ExecuteReader();
						if (dr_acepcion.Read())
						{
							id_acepcion = dr_acepcion.GetInt32(0);
							contenido_lemas_encontrados += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")</a></li>";
						}
					}
					catch
					{

					}
					count++;
				}
				if (count == 0)
				{

					contenido_lemas_encontrados = "";
					contenido_lemas_encontrados += "<h1 style = \"text-align: justify; font-size:large;\">";
					contenido_lemas_encontrados += "No se han encontrado resultados";
					contenido_lemas_encontrados += "</h1>";
				}
				if (count == 1)
				{
					connection.Close();
					return "AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + lema;
				}
				contenido_lemas_encontrados += "</ul>";
			}
			catch
			{
			}
			finally
			{
				connection.Close();
			}
			return contenido_lemas_encontrados;
		}

		public static String Busqueda_Conjugaciones (OleDbConnection connection, String word)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE (lema LIKE '%" + AuxiliarTools.Word_converter(word) + "%' AND id_lema IN (SELECT id_lema FROM Acepciones WHERE id_categoria = 3)) ORDER BY lema,etimología;", connection);
			String contenido_lemas_encontrados = "";
			connection.Open();
			try
			{
				OleDbDataReader dr = command.ExecuteReader();
				contenido_lemas_encontrados = "";
				contenido_lemas_encontrados += "<ul style = \"font-size:large;\">";
				long id_lema = 0;
				String lema = "";
				String etimologia;
				int count = 0;
				while (dr.Read())
				{
					id_lema = dr.GetInt32(0);
					lema = dr.GetString(1).ToString();
					etimologia = dr.GetString(2).ToString();
					contenido_lemas_encontrados += "<li><a href='ConjugacionesVerbales.aspx?word=" + lema + "&lema=" + id_lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")</a></li>";
					count++;
				}
				if (count == 0)
				{
					contenido_lemas_encontrados = "";
					contenido_lemas_encontrados += "<h1 style = \"text-align: justify; font-size:large;\">";
					contenido_lemas_encontrados += "No se han encontrado resultados";
					contenido_lemas_encontrados += "</h1>";
					connection.Close();
				}
				if (count == 1)
				{
					connection.Close();
					return "ConjugacionesVerbales.aspx?word=" + lema + "&lema=" + id_lema;
				}
				contenido_lemas_encontrados += "</ul>";
			}
			catch
			{
				connection.Close();
			}
			return contenido_lemas_encontrados;
		}

		public static String Busqueda_Campos (OleDbConnection connection, String word)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_campo_tematico,campo_tematico,id_campo_padre,nivel FROM CamposTematicos WHERE campo_tematico LIKE '%" + AuxiliarTools.Word_converter(word) + "%' ORDER BY campo_tematico;", connection);
			connection.Open();
			String contenido_campos_encontrados = "";
			int id_campo = 0;
			String campo_tematico = "";
			int campo_padre = 0;
			int nivel = 0;
			try
			{
				OleDbDataReader dr = command.ExecuteReader();
				contenido_campos_encontrados = "";
				contenido_campos_encontrados += "<ul style = \"font-size:large;\">";
				int count = 0;
				while (dr.Read())
				{
					id_campo = dr.GetInt32(0);
					campo_tematico = dr.GetString(1);
					campo_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_campos_encontrados += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo + "&id_campo_padre=" + campo_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico) + "</a></li>";
					count++;
				}
				if (count == 0)
				{
					contenido_campos_encontrados = "";
					contenido_campos_encontrados += "<h1 style = \"text-align: justify; font-size:large;\">";
					contenido_campos_encontrados += "No se han encontrado resultados";
					contenido_campos_encontrados += "</h1>";
					connection.Close();
				}
				if (count == 1)
				{
					return "CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo + "&id_campo_padre=" + campo_padre + "&nivel=" + nivel;
				}
				contenido_campos_encontrados += "</ul>";
			}
			catch
			{
				connection.Close();
			}
			return contenido_campos_encontrados;
		}

		public static String Busqueda_Localizaciones(OleDbConnection connection, String word)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_localizacion,localizacion,id_loc_padre,nivel FROM Localizaciones WHERE localizacion LIKE '%" + AuxiliarTools.Word_converter(word) + "%' ORDER BY localizacion;", connection);
			connection.Open();
			String contenido_localizaciones_encontradas = "";
			int id_localizacion = 0;
			String localizacion = "";
			int localizacion_padre = 0;
			int nivel = 0;
			try
			{
				OleDbDataReader dr = command.ExecuteReader();
				contenido_localizaciones_encontradas = "";
				contenido_localizaciones_encontradas += "<ul style = \"font-size:large;\">";
				int count = 0;
				while (dr.Read())
				{
					id_localizacion = dr.GetInt32(0);
					localizacion = dr.GetString(1);
					localizacion_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_localizaciones_encontradas += "<li><a href='Localizaciones.aspx?word=" + localizacion + "&id_localizacion=" + id_localizacion + "&id_loc_padre=" + localizacion_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(localizacion) + "</a></li>";
					count++;
				}
				if (count == 0)
				{
					contenido_localizaciones_encontradas = "";
					contenido_localizaciones_encontradas += "<h1 style = \"text-align: justify; font-size:large;\">";
					contenido_localizaciones_encontradas += "No se han encontrado resultados";
					contenido_localizaciones_encontradas += "</h1>";
					connection.Close();
				}
				if (count == 1)
				{
					return "Localizaciones.aspx?word=" + localizacion + "&id_localizacion=" + id_localizacion + "&id_loc_padre=" + localizacion_padre + "&nivel=" + nivel;
				}
				contenido_localizaciones_encontradas += "</ul>";
			}
			catch
			{
				connection.Close();
			}
			return contenido_localizaciones_encontradas;
		}

		public static String Acepciones_de_campos_hijos (int id_campo_tematico, OleDbConnection connection)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_acepcion FROM Acepcion_CampoTematico WHERE id_campo_tematico=" + id_campo_tematico + ";", connection); ;
			OleDbDataReader dr;
			OleDbCommand command_lema;
			OleDbDataReader dr_lema;
			OleDbCommand command_significado;
			OleDbDataReader dr_significado;
			String lema = "";
			String significado = "";
			String contenido_campo_elegido = "";
			int id_acepcion;
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					id_acepcion = dr.GetInt32(0);
					command_lema = new OleDbCommand("SELECT lema FROM Lemas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion + ")", connection);
					try
					{
						dr_lema = command_lema.ExecuteReader();
						if (dr_lema.Read())
						{
							lema = dr_lema.GetString(0);
						}
					}
					catch { }
					command_significado = new OleDbCommand("SELECT significado FROM Significados WHERE id_significado IN (SELECT id_significado FROM Acepcion_Significado WHERE id_acepcion=" + id_acepcion + ")", connection);
					try
					{
						dr_significado = command_significado.ExecuteReader();
						if (dr_significado.Read())
						{
							significado = dr_significado.GetString(0);
						}
					}
					catch { }
					contenido_campo_elegido += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</a>: " + significado + "</li>";
				}
			}
			catch { }
			command = new OleDbCommand("SELECT id_campo_tematico FROM CamposTematicos WHERE id_campo_padre=" + id_campo_tematico + ";",connection);
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					contenido_campo_elegido += Acepciones_de_campos_hijos(dr.GetInt32(0),connection);
				}
			}
			catch {}
			return contenido_campo_elegido;
		}
		public static String Acepciones_de_localizaciones_hijas(int id_localizacion, OleDbConnection connection)
		{
			OleDbCommand command = new OleDbCommand("SELECT id_acepcion FROM Acepcion_Localizacion WHERE id_localizacion=" + id_localizacion + ";", connection); ;
			OleDbDataReader dr;
			OleDbCommand command_lema;
			OleDbDataReader dr_lema;
			OleDbCommand command_significado;
			OleDbDataReader dr_significado;
			String lema = "";
			String significado = "";
			String contenido_localizacion_elegida = "";
			int id_acepcion;
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					id_acepcion = dr.GetInt32(0);
					command_lema = new OleDbCommand("SELECT lema FROM Lemas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion + ")", connection);
					try
					{
						dr_lema = command_lema.ExecuteReader();
						if (dr_lema.Read())
						{
							lema = dr_lema.GetString(0);
						}
					}
					catch { }
					command_significado = new OleDbCommand("SELECT significado FROM Significados WHERE id_significado IN (SELECT id_significado FROM Acepcion_Significado WHERE id_acepcion=" + id_acepcion + ")", connection);
					try
					{
						dr_significado = command_significado.ExecuteReader();
						if (dr_significado.Read())
						{
							significado = dr_significado.GetString(0);
						}
					}
					catch { }
					contenido_localizacion_elegida += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</a>: " + significado + "</li>";
				}
			}
			catch { }
			command = new OleDbCommand("SELECT id_localizacion FROM Localizaciones WHERE id_loc_padre=" + id_localizacion + ";", connection);
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					contenido_localizacion_elegida += Acepciones_de_localizaciones_hijas(dr.GetInt32(0), connection);
				}
			}
			catch { }
			return contenido_localizacion_elegida;
		}

		public static String Campos_orden_jerarquico(int nivel,OleDbConnection con, int id_campo_tematico_padre, int id_campo)
		{
			String result = "";
			OleDbCommand command;
			if (id_campo_tematico_padre == 0)
			{
				command = new OleDbCommand("SELECT id_campo_tematico, campo_tematico, id_campo_padre FROM CamposTematicos WHERE nivel=" + nivel + " ORDER BY campo_tematico", con);
			}
			else
			{
				command = new OleDbCommand("SELECT id_campo_tematico, campo_tematico, id_campo_padre FROM CamposTematicos WHERE nivel=" + nivel + " AND id_campo_padre=" + id_campo_tematico_padre + " ORDER BY campo_tematico", con);
			}
			OleDbDataReader dr;
			int id_campo_tematico;
			String campo_tematico;
			int id_campo_padre;
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					id_campo_tematico = dr.GetInt32(0);
					campo_tematico = dr.GetString(1);
					id_campo_padre = dr.GetInt32(2);
					String tab = "";
					for (int i = 0;i < nivel; i++)
					{
						tab += "&nbsp&nbsp&nbsp&nbsp";
					}
					result += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo_tematico + "&id_campo_padre=" + id_campo_padre + "&nivel=" + nivel + "'>" + tab;
					if (id_campo == id_campo_tematico)
					{
						result += "<b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico) + "</b>";
					}
					else
					{
						result += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico);
					}
					result += "</a></li>" + Campos_orden_jerarquico(nivel + 1, con,id_campo_tematico,id_campo);
				}
			}
			catch { }
			return result;
		}
		public static String Localizaciones_orden_jerarquico(int nivel, OleDbConnection con,int id_localizacion_padre,int id_loc)
		{
			String result = "";
			OleDbCommand command;
			if (id_localizacion_padre == 0)
			{
				command = new OleDbCommand("SELECT id_localizacion, localizacion, id_loc_padre FROM Localizaciones WHERE nivel=" + nivel + " ORDER BY localizacion", con);
			}
			else
			{
				command = new OleDbCommand("SELECT id_localizacion, localizacion, id_loc_padre FROM Localizaciones WHERE nivel=" + nivel + " AND id_loc_padre=" + id_localizacion_padre + " ORDER BY localizacion", con);
			}
			OleDbDataReader dr;
			int id_localizacion;
			String localizacion;
			int id_loc_padre;
			try
			{
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					id_localizacion = dr.GetInt32(0);
					localizacion = dr.GetString(1);
					id_loc_padre = dr.GetInt32(2);
					String tab = "";
					for (int i = 0; i < nivel; i++)
					{
						tab += "&nbsp&nbsp&nbsp&nbsp";
					}
					result += "<li><a href='Localizaciones.aspx?word=" + localizacion + "&id_localizacion=" + id_localizacion + "&id_loc_padre=" + id_loc_padre + "&nivel=" + nivel + "'>" + tab;
					if (id_loc == id_localizacion)
					{
						result += "<b>" + localizacion + "</b>";
					}
					else
					{
						result += localizacion;
					}
					result += "</a></li>" + Localizaciones_orden_jerarquico(nivel + 1, con, id_localizacion, id_loc);
				}
			}
			catch { }
			return result;
		}
	}
}