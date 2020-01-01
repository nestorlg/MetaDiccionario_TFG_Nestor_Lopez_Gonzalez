using System;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class AcepcionElegida : System.Web.UI.Page
	{
		OleDbConnection connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbCommand command;
		String contenido_acepcion_elegida;
		String contenido_acepciones_del_lema;
		int id_acepcion;
		OleDbDataReader dr;
		String contenido_informacion_extra;
		OleDbDataReader dr_categoria;
		OleDbDataReader dr_numero;
		OleDbDataReader dr_genero;
		OleDbDataReader dr_significado;
		OleDbDataReader dr_ejemplos;
		int id_categoria;
		int id_numero;
		int id_genero;
		String categoria;
		String numero;
		String genero;
		String significado;
		String lema;
		int opcion;
		Tree tree;
		static bool mas_info = true;

		private void Definicion_general()
		{
			connection.Open();
			command = new OleDbCommand("SELECT id_categoria,id_numero,id_genero FROM Acepciones WHERE id_acepcion = " + id_acepcion + ";", connection);
			try
			{
				dr = command.ExecuteReader();
				if (dr.Read())
				{
					id_categoria = dr.GetInt32(0);
					id_numero = dr.GetInt32(1);
					id_genero = dr.GetInt32(2);
					command = new OleDbCommand("SELECT categoria FROM Categorias WHERE id_categoria = " + id_categoria + ";", connection);
					dr_categoria = command.ExecuteReader();
					command = new OleDbCommand("SELECT numero FROM Numero WHERE id_numero = " + id_numero + ";", connection);
					dr_numero = command.ExecuteReader();
					command = new OleDbCommand("SELECT genero FROM Generos WHERE id_genero = " + id_genero + ";", connection);
					dr_genero = command.ExecuteReader();
					command = new OleDbCommand("SELECT significado FROM Significados WHERE id_significado IN (SELECT id_significado FROM Acepcion_Significado WHERE id_acepcion = " + id_acepcion + ");", connection);
					dr_significado = command.ExecuteReader();
					try
					{
						if (dr_categoria.Read() && dr_numero.Read() && dr_genero.Read() && dr_significado.Read())
						{
							categoria = dr_categoria.GetString(0);
							numero = dr_numero.GetString(0);
							genero = dr_genero.GetString(0);
							significado = dr_significado.GetString(0);
							contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:&nbsp";
							contenido_acepcion_elegida += significado + "<br /><br />";
							command = new OleDbCommand("SELECT ejemplo FROM EjemplosAcepciones WHERE id_acepcion = " + id_acepcion + ";",connection);
							try
							{
								dr_ejemplos = command.ExecuteReader();
								String ejemplo;
								contenido_acepcion_elegida += "Ejemplos:<br/><br/><ul>";
								while (dr_ejemplos.Read())
								{
									ejemplo = dr_ejemplos.GetString(0);
									contenido_acepcion_elegida += "<li>" + ejemplo + "</li>";
								}
								contenido_acepcion_elegida += "</ul><br /><br />";
							}
							catch {}
							contenido_acepcion_elegida += "Categoría: " + categoria + "<br/>";
							contenido_acepcion_elegida += "Número: " + numero + "<br/>";
							contenido_acepcion_elegida += "Género: " + genero + "<br/></h1>";
						}
					}
					catch {}
				}
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}

		private void Sinonimos()
		{
			connection.Open();
			OleDbCommand command_sinonimos = new OleDbCommand("SELECT id_significado FROM Semantica WHERE (id_acepcion_origen=" + id_acepcion + " OR id_acepcion_relacionada=" + id_acepcion + ");", connection);
			OleDbDataReader dr_sinonimos;

			int id_significado;
			try
			{
				dr_sinonimos = command_sinonimos.ExecuteReader();
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				OleDbCommand command_acepciones;
				OleDbDataReader dr_acepciones;
				OleDbCommand command_otra_acepcion;
				OleDbDataReader dr_otra_acepcion;
				int id_acepcion_origen;
				int id_acepcion_relacionada;
				String lema_sinonimo;
				int count = 0;
				while (dr_sinonimos.Read())
				{
					id_significado = dr_sinonimos.GetInt32(0);
					command_acepciones = new OleDbCommand("SELECT id_acepcion_origen,id_acepcion_relacionada FROM semantica WHERE id_significado=" + id_significado + ";",connection);
					try
					{
						dr_acepciones = command_acepciones.ExecuteReader();
						int id_otra_acepcion;
						while (dr_acepciones.Read())
						{
							id_acepcion_origen = dr_acepciones.GetInt32(0);
							id_acepcion_relacionada = dr_acepciones.GetInt32(1);

							if (id_acepcion == id_acepcion_origen)
							{
								command_otra_acepcion = new OleDbCommand("SELECT lema FROM Lemas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion_relacionada + ");",connection);
								id_otra_acepcion = id_acepcion_relacionada;
							}
							else
							{
								command_otra_acepcion = new OleDbCommand("SELECT lema FROM Lemas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion_origen + ");", connection);
								id_otra_acepcion = id_acepcion_origen;
							}
							try
							{
								dr_otra_acepcion = command_otra_acepcion.ExecuteReader();
								contenido_acepcion_elegida += "<ul>";
								if (dr_otra_acepcion.Read())
								{
									lema_sinonimo = dr_otra_acepcion.GetString(0);
									contenido_acepcion_elegida += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_otra_acepcion + "&lema=" + lema_sinonimo + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema_sinonimo) + "</a></li>";
								}
								contenido_acepcion_elegida += "</ul></h1>";
							}
							catch {}
						}
						count++;
					}
					catch {}
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado sinónimos para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul>";
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}
		private void Formas_flexionadas()
		{
			connection.Open();
			command = new OleDbCommand("SELECT id_flexion,palabra FROM Acepcion_Flexion WHERE id_acepcion=" + id_acepcion + ";", connection);

			int id_flexion;
			String palabra;
			try
			{
				dr = command.ExecuteReader();
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				OleDbCommand command_tipo_flexion;
				OleDbDataReader dr_tipo_flexion;
				String tipo_flexion = "";
				int count = 0;
				while (dr.Read())
				{
					id_flexion = dr.GetInt32(0);
					palabra = dr.GetString(1);

					command_tipo_flexion = new OleDbCommand("SELECT flexión FROM Flexiones WHERE id_flexion=" + id_flexion + ";", connection);
					dr_tipo_flexion = command_tipo_flexion.ExecuteReader();
					if (dr_tipo_flexion.Read())
					{
						tipo_flexion = dr_tipo_flexion.GetString(0);
					}
					contenido_acepcion_elegida += "<li>" + tipo_flexion + ": <b>" + palabra + "</b></li>";
					count++;
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado formas flexionadas para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul></h1>";
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}

		private void Palabras_derivadas()
		{
			connection.Open();
			command = new OleDbCommand("SELECT id_proceso,palabra FROM Derivadas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion + ");", connection);

			int id_proceso;
			String palabra;
			try
			{
				dr = command.ExecuteReader();
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				OleDbCommand command_tipo_flexion;
				OleDbDataReader dr_proceso_derivativo;
				String proceso_derivativo = "";
				int count = 0;
				while (dr.Read())
				{
					id_proceso = dr.GetInt32(0);
					palabra = dr.GetString(1);

					command_tipo_flexion = new OleDbCommand("SELECT proceso FROM ProcesosDerivativos WHERE id_proceso=" + id_proceso + ";", connection);
					dr_proceso_derivativo = command_tipo_flexion.ExecuteReader();
					if (dr_proceso_derivativo.Read())
					{
						proceso_derivativo = dr_proceso_derivativo.GetString(0);
					}
					contenido_acepcion_elegida += "<li>" + proceso_derivativo + ": <b>" + palabra + "</b></li>";
					count++;
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado palabras derivadas para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul></h1>";
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}

		private void Dudas_linguisticas()
		{
			connection.Open();
			command = new OleDbCommand("SELECT duda FROM Dudas WHERE id_duda IN (SELECT id_duda FROM Acepcion_Duda WHERE id_acepcion=" + id_acepcion + ");",connection);

			String duda;
			try
			{
				dr = command.ExecuteReader();
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				int count = 0;
				while (dr.Read())
				{
					duda = dr.GetString(0);
					contenido_acepcion_elegida += "<li>" + duda + "</li>";
					count++;
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado dudas lingüísticas para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul></h1>";
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}

		private void Locuciones_colocaciones()
		{
			connection.Open();
			command = new OleDbCommand("SELECT colocacion FROM Colocaciones WHERE id_colocacion IN (SELECT id_colocacion FROM Lema_Colocacion WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion + "));",connection);
			
			String colocacion;
			try
			{
				dr = command.ExecuteReader();
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				int count = 0;
				while (dr.Read())
				{
					colocacion = dr.GetString(0);
					contenido_acepcion_elegida += "<li>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(colocacion) + "</li>";
					count++;
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado colocaciones para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul>";

				OleDbCommand command_locuciones = new OleDbCommand("SELECT id_locucion,locucion FROM Locuciones WHERE id_locucion IN (SELECT id_colocacion FROM Lema_Colocacion WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion=" + id_acepcion + "));", connection);

				try
				{
					OleDbDataReader dr_locuciones = command_locuciones.ExecuteReader();
					contenido_acepcion_elegida += "<br /><br /><ul>";
					OleDbCommand command_ejemplos_locuciones;
					OleDbDataReader dr_ejemplos_locuciones;
					int id_locucion;
					String locucion;
					String ejemplo_locucion;
					count = 0;
					while (dr_locuciones.Read())
					{
						id_locucion = dr.GetInt32(0);
						locucion = dr_locuciones.GetString(1);
						contenido_acepcion_elegida += "<li>" + locucion + "</li>";

						command_ejemplos_locuciones = new OleDbCommand("SELECT ejemplo FROM EjemplosLocuciones WHERE id_locucion=" + id_locucion + ";", connection);
						try
						{
							dr_ejemplos_locuciones = command_ejemplos_locuciones.ExecuteReader();
							contenido_acepcion_elegida += "<br /><ul>";
							while (dr_ejemplos_locuciones.Read())
							{
								ejemplo_locucion = dr_ejemplos_locuciones.GetString(0);
								contenido_acepcion_elegida += "<li>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ejemplo_locucion) + "</li>";
							}
							contenido_acepcion_elegida += "</ul></h1>";
						}
						catch {}
						acepcion_elegida.Text = contenido_acepcion_elegida;
						connection.Close();
						count++;
					}
					if (count == 0)
					{
						contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado locuciones para este término</h1>";
					}
				}
				catch {}
				acepcion_elegida.Text = contenido_acepcion_elegida;
				connection.Close();
			}
			catch {}
			connection.Close();
		}

		private void Campos_tematicos()
		{
			connection.Open();
			contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><br/><b>Campos temáticos:</b><br/><br/>";
			command = new OleDbCommand("SELECT campo_tematico, id_campo_tematico, id_campo_padre, nivel FROM CamposTematicos WHERE id_campo_tematico IN (SELECT id_campo_tematico FROM Acepcion_CampoTematico WHERE id_acepcion=" + id_acepcion + ");", connection);
			String campo;
			int id_campo;
			int id_campo_padre;
			int nivel;
			try
			{
				tree = new Tree(0,0);
				dr = command.ExecuteReader();
				while (dr.Read())
				{
					campo = dr.GetString(0);
					id_campo = dr.GetInt32(1);
					id_campo_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					tree = AuxiliarTools.FindFatherThematicFields(campo, id_campo, id_campo_padre, nivel, tree, connection);
					contenido_acepcion_elegida += tree.PrintTree();
				}
				contenido_acepcion_elegida += "</h1>";
			}
			catch {}
			connection.Close();
			acepcion_elegida.Text = contenido_acepcion_elegida;
		}

		private void Preposiciones()
		{
			connection.Open();
			command = new OleDbCommand("SELECT preposicion FROM Preposiciones WHERE id_preposicion IN (SELECT id_preposicion FROM Acepcion_Preposicion WHERE id_acepcion=" + id_acepcion + ");", connection);

			String preposicion;
			try
			{
				dr = command.ExecuteReader();
				int count = 0;
				contenido_acepcion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b>:<br /><br /><br /><ul>";
				while (dr.Read())
				{
					preposicion = dr.GetString(0);
					contenido_acepcion_elegida += "<li>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " " + preposicion + "</li>";
					count++;
				}
				if (count == 0)
				{
					contenido_acepcion_elegida = "<h1 style = \"text-align: justify; font-size:large;\">No se han encontrado preposiciones para este término</h1>";
				}
				contenido_acepcion_elegida += "</ul></h1>";
			}
			catch {}
			acepcion_elegida.Text = contenido_acepcion_elegida;
			connection.Close();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			id_acepcion = Convert.ToInt32(Request.QueryString["acepcion"]);
			lema = Request.QueryString["lema"];

			opcion = Convert.ToInt32(Opciones.SelectedValue);
			lema_elegido.Text = "<h1 style = \"text-align: center; font-size:24pt;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + "</b></h1>";
			acepcion_elegida.Text = "";
			Mas_Informacion.Text = "Más";

			switch (opcion)
			{
				case 0:
					Definicion_general();
					Mas_Informacion.Visible = true;
					informacion_extra.Text = "";
					break;
				case 1:
					Sinonimos();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 2:
					Formas_flexionadas();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 3:
					Palabras_derivadas();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 4:
					Dudas_linguisticas();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 5:
					Locuciones_colocaciones();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 6:
					Campos_tematicos();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				case 7:
					Preposiciones();
					Mas_Informacion.Visible = false;
					informacion_extra.Text = "";
					break;
				default:
					break;
			}

			connection.Open();

			OleDbCommand command_imagen = new OleDbCommand("SELECT imagen_url FROM Acepciones WHERE id_acepcion=" + id_acepcion + ";", connection);
			try
			{
				OleDbDataReader dr_imagen = command_imagen.ExecuteReader();
				if (dr_imagen.Read())
				{
					Imagen.ImageUrl = "~/bd_images/" + dr_imagen.GetString(0);
				}
			}
			catch (Exception)
			{
				Imagen.AlternateText = "Imagen no disponible";
			}

			command = new OleDbCommand("SELECT id_acepcion,NumAcepcion FROM Acepciones WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_acepcion = " + id_acepcion + ");",connection);
			try
			{
				dr = command.ExecuteReader();
				OleDbCommand command_for_significados;
				String[] significados;
				int i;
				int num_acepcion;
				int id_acep;
				contenido_acepciones_del_lema = "";
				contenido_acepciones_del_lema += "<p style = \"font-size:large;\">";
				while (dr.Read())
				{
					id_acep = dr.GetInt32(0);
					num_acepcion = dr.GetInt32(1);
					command_for_significados = new OleDbCommand("SELECT significado FROM Significados WHERE id_significado IN (SELECT id_significado FROM Acepcion_Significado WHERE id_acepcion = " + id_acep + ");", connection);
					try
					{
						dr_significado = command_for_significados.ExecuteReader();
						contenido_acepciones_del_lema += "<button type='button' style='padding: 5px;' onclick='window.location.href=\"AcepcionElegida.aspx?acepcion=" + id_acep + "&lema=" + lema + "\"' class='btn btn-primary'>&nbsp" + num_acepcion + "</button>";
						if (id_acep == id_acepcion)
						{
							contenido_acepciones_del_lema += "<b>";
						}
						if (dr_significado.Read())
						{
							significados = dr_significado.GetString(0).Split(' ');
							if (significados.Length > 10)
							{
								for (i = 0; i < 10; i++)
								{
									if (i > 0)
									{
										contenido_acepciones_del_lema += " ";
									}
									contenido_acepciones_del_lema += significados[i];
								}
								contenido_acepciones_del_lema += "...";
							}
							else
							{
								contenido_acepciones_del_lema += dr_significado.GetString(0);
							}
						}
						if (id_acep == id_acepcion)
						{
							contenido_acepciones_del_lema += "</b>";
						}
						contenido_acepciones_del_lema += "<br/><br/>";
					}
					catch
					{

					}
				}
				contenido_acepciones_del_lema += "</p>";
			}
			catch {}
			connection.Close();
			acepciones_del_lema.Text = contenido_acepciones_del_lema;
		}
		protected void Button_Lemas_Click(object sender, EventArgs e)
		{
			Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Lemas.Text.Trim());
		}

		protected void Mas_Informacion_Click(object sender, EventArgs e)
		{
			if (mas_info)
			{
				connection.Open();
				command = new OleDbCommand("SELECT id_lema FROM Acepciones WHERE id_acepcion = " + id_acepcion + ";", connection);
				dr = command.ExecuteReader();
				int id_lema;
				int i = 0;

				contenido_informacion_extra = "";
				contenido_informacion_extra += "<h1 style = \"text-align: justify; font-size:large;\"><br /><table class='table table-striped'><thead><tr><th>Etimología</th><th>Tipo silábico</th><th>Sílabas</th><th>Fonética</th><th>Préstamo</th></tr></thead>";
				
				try
				{
					if (dr.Read())
					{
						id_lema = dr.GetInt32(0);
						command = new OleDbCommand("SELECT etimología,tipo_silabico,sílabas,fonética,prestamo FROM Lemas WHERE id_lema = " + id_lema + ";", connection);
						dr = command.ExecuteReader();
						String etimologia;
						String tipo_silabico;
						String silabas;
						String fonetica;
						Boolean prestamo;
						try
						{
							if (dr.Read())
							{
								etimologia = dr.GetString(0);
								tipo_silabico = dr.GetString(1);
								silabas = dr.GetString(2);
								fonetica = dr.GetString(3);
								prestamo = dr.GetBoolean(4);

								contenido_informacion_extra += "<tbody><tr><td>" + etimologia + "</td><td>" + tipo_silabico + "</td><td>" + silabas + "</td><td>" + fonetica + "</td><td>";

								if (prestamo)
								{
									contenido_informacion_extra += "Sí";
								}
								else
								{
									contenido_informacion_extra += "No";
								}
								contenido_informacion_extra += "</td></tbody></table></h1>";
							}
						}
						catch {}

						command = new OleDbCommand("SELECT marca FROM Marcas WHERE id_marca IN (SELECT id_marca FROM Acepcion_Marca WHERE id_acepcion=" + id_acepcion + ");", connection);
						dr = command.ExecuteReader();
						String marca;
						String contenido_marcas = "<h1 style = \"text-align: justify; font-size:large;\"><br /><br /><table class='table table-striped'><thead><tr><th>Marcas</th></tr></thead>";
						try
						{
							contenido_marcas += "<tbody><tr>";
							i = 0;
							while (dr.Read())
							{
								marca = dr.GetString(0);
								contenido_marcas += "<td>" + marca + "</td>";
							}
							contenido_marcas += "</tr></tbody></table></h1>";
							if (i == 0)
							{
								contenido_marcas = "No se han encontrado marcas relacionadas con esta acepción";
							}
							contenido_informacion_extra += contenido_marcas;
						}
						catch {}

						command = new OleDbCommand("SELECT fuente FROM Fuentes WHERE id_fuente IN (SELECT id_fuente FROM Acepcion_Fuente WHERE id_acepcion=" + id_acepcion + ");", connection);
						dr = command.ExecuteReader();
						String fuente;
						String contenido_fuentes = "<h1 style = \"text-align: justify; font-size:large;\"><br /><br /><table class='table table-striped'><thead><tr><th>Fuentes</th></tr></thead>";
						try
						{
							contenido_fuentes += "<tbody><tr>";
							i = 0;
							while (dr.Read())
							{
								fuente = dr.GetString(0);
								contenido_fuentes += "<td>" + fuente + "</td>";
								i++;
							}
							contenido_fuentes += "</tr></tbody></table><h1>";
							if (i == 0)
							{
								contenido_fuentes = "<h1 style = \"text-align: justify; font-size:large;\"><br /><b>No se han encontrado fuentes relacionadas con esta acepción</b></h1>";
							}
							contenido_informacion_extra += contenido_fuentes;
						}
						catch {}

						contenido_informacion_extra += "<h1 style = \"text-align: justify; font-size:large;\"><br/><b>Localizaciones:</b><br/><br/>";
						command = new OleDbCommand("SELECT localizacion, id_localizacion, id_loc_padre, nivel FROM Localizaciones WHERE id_localizacion IN (SELECT id_localizacion FROM Acepcion_Localizacion WHERE id_acepcion=" + id_acepcion + ");", connection);
						String localizacion;
						int id_localizacion;
						int id_loc_padre;
						int nivel;
						try
						{
							tree = new Tree(0,1);
							dr = command.ExecuteReader();
							while (dr.Read())
							{
								localizacion = dr.GetString(0);
								id_localizacion = dr.GetInt32(1);
								id_loc_padre = dr.GetInt32(2);
								nivel = dr.GetInt32(3);
								tree = AuxiliarTools.FindFatherLocations(localizacion, id_localizacion, id_loc_padre, nivel, tree, connection);
								contenido_informacion_extra += tree.PrintTree();
							}
						}
						catch {}
						contenido_informacion_extra += "</h1>";
					}
				}
				catch {}
				informacion_extra.Text = contenido_informacion_extra;
				connection.Close();
				Mas_Informacion.Text = "Menos";
				mas_info = false;
			}
			else
			{
				informacion_extra.Text = "";
				Mas_Informacion.Text = "Más";
				mas_info = true;
			}
		}
	}
}
 