using System;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class PalabrasEncontradas : System.Web.UI.Page
	{
		OleDbConnection	connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbCommand command;
		OleDbDataReader dr;
		String word;
		String contenido_lemas_encontrados;
		String contenido_verbos_encontrados;
		String contenido_campos_encontrados;
		String contenido_localizaciones_encontradas;
		int opcion = 0;
		protected void Page_Load(object sender, EventArgs e)
		{
			connection.Open();
			word = Request.QueryString["word"];
			opcion = Convert.ToInt32(Opciones.SelectedValue);
			if (word.Length > 2)
			{
				command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE lema LIKE '%" + AuxiliarTools.Word_converter(word) + "%' ORDER BY lema;", connection);
			}
			else
			{
				command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE lema='" + AuxiliarTools.Word_converter(word) + "' ORDER BY lema;", connection);
			}
			try
			{
				dr = command.ExecuteReader();
				int id_lema;
				String lema;
				String etimologia;
				contenido_lemas_encontrados = "<ul style = \"text-align: justify; font-size:large;\">";
				OleDbCommand command_aux;
				OleDbDataReader dr_aux;
				int id_acepcion;
				while (dr.Read())
				{
					id_lema = dr.GetInt32(0);
					lema = dr.GetString(1);
					etimologia = dr.GetString(2);
					command_aux = new OleDbCommand("SELECT id_acepcion FROM Acepciones WHERE id_lema=" + id_lema + " AND NumAcepcion=1",connection);
					try
					{
						dr_aux = command_aux.ExecuteReader();
						if (dr_aux.Read())
						{
							id_acepcion = dr_aux.GetInt32(0);
							contenido_lemas_encontrados += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")" + "</a></li>";
						}
					}
					catch {}
				}
				contenido_lemas_encontrados += "</ul>";
				lemas_encontrados.Text = contenido_lemas_encontrados;
				if (opcion == 0)
				{
					columna_elegida.Text = contenido_lemas_encontrados;
					opcion_elegida.Text = "<b>Acepciones del diccionario</b>";
				}
			}
			catch (Exception exc)
			{
				lemas_encontrados.Text = exc.ToString();
			}
			if (word.Length > 2)
			{
				command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE lema LIKE '%" + word + "%' AND id_lema IN (SELECT id_lema FROM Acepciones WHERE id_categoria=3) ORDER BY lema;", connection);
			}
			else
			{
				command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE lema='" + word + "' AND id_lema IN (SELECT id_lema FROM Acepciones WHERE id_categoria=3) ORDER BY lema;", connection);
			}
			try
			{
				dr = command.ExecuteReader();
				int id_lema;
				String lema;
				String etimologia;
				contenido_verbos_encontrados = "<ul style = \"text-align: justify; font-size:large;\">";
				while (dr.Read())
				{
					id_lema = dr.GetInt32(0);
					lema = dr.GetString(1);
					etimologia = dr.GetString(2);
					contenido_verbos_encontrados += "<li><a href='ConjugacionesVerbales.aspx?word=" + lema + "&lema=" + id_lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")" + "</a></li>";
				}
				contenido_verbos_encontrados += "</ul>";
				verbos_encontrados.Text = contenido_verbos_encontrados;
				if (opcion == 1)
				{
					columna_elegida.Text = contenido_verbos_encontrados;
					opcion_elegida.Text = "<b>Conjugaciones verbales</b>";
				}
			}
			catch (Exception exc)
			{
				verbos_encontrados.Text = exc.ToString();
			}
			if (word.Length > 2)
			{
				command = new OleDbCommand("SELECT campo_tematico,id_campo_tematico,id_campo_padre,nivel FROM CamposTematicos WHERE campo_tematico LIKE '%" + word + "%' ORDER BY campo_tematico;", connection);
			}
			else
			{
				command = new OleDbCommand("SELECT campo_tematico,id_campo_tematico,id_campo_padre,nivel FROM CamposTematicos WHERE campo_tematico='" + word + "' ORDER BY campo_tematico;", connection);
			}
			try
			{
				dr = command.ExecuteReader();
				String campo_tematico;
				int id_campo_tematico;
				int id_campo_padre;
				int nivel;
				contenido_campos_encontrados = "<ul style = \"text-align: justify; font-size:large;\">";
				while (dr.Read())
				{
					campo_tematico = dr.GetString(0);
					id_campo_tematico = dr.GetInt32(1);
					id_campo_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_campos_encontrados += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo_tematico + "&id_campo_padre=" + id_campo_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico) + "</a></li>";
				}
				contenido_campos_encontrados += "</ul>";
				campos_encontrados.Text = contenido_campos_encontrados;
				if (opcion == 2)
				{
					columna_elegida.Text = contenido_campos_encontrados;
					opcion_elegida.Text = "<b>Campos temáticos</b>";
				}
			}
			catch (Exception exc)
			{
				campos_encontrados.Text = exc.ToString();
			}
			if (word.Length > 2)
			{
				command = new OleDbCommand("SELECT localizacion,id_localizacion,id_loc_padre,nivel FROM Localizaciones WHERE localizacion LIKE '%" + word + "%' ORDER BY localizacion;", connection);
			}
			else
			{
				command = new OleDbCommand("SELECT localizacion,id_localizacion,id_loc_padre,nivel FROM Localizaciones WHERE localizacion='" + word + "' ORDER BY localizacion;", connection);
			}
			try
			{
				dr = command.ExecuteReader();
				String localizacion;
				int id_localizacion;
				int id_localizacion_padre;
				int nivel;
				contenido_localizaciones_encontradas = "<ul style = \"text-align: justify; font-size:large;\">";
				while (dr.Read())
				{
					localizacion = dr.GetString(0);
					id_localizacion = dr.GetInt32(1);
					id_localizacion_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_localizaciones_encontradas += "<li><a href='Localizaciones.aspx?word=" + localizacion + "&id_localizacion=" + id_localizacion + "&id_loc_padre=" + id_localizacion_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(localizacion) + "</a></li>";
				}
				contenido_localizaciones_encontradas += "</ul>";
				localizaciones_encontradas.Text = contenido_localizaciones_encontradas;
				if (opcion == 3)
				{
					columna_elegida.Text = contenido_localizaciones_encontradas;
					opcion_elegida.Text = "<b>Localizaciones</b>";
				}
			}
			catch (Exception exc)
			{
				localizaciones_encontradas.Text = exc.ToString();
			}
			connection.Close();
		}
		protected void Button_Lemas_Click(object sender, EventArgs e)
		{
			Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Lemas.Text.Trim());
		}

		protected void Change_Option(object sender, EventArgs e)
		{
			Response.Redirect("PalabrasEncontradas.aspx?word=" + word);
		}
	}
}