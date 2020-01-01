using System;
using System.Web.UI;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class _Default : Page
	{
		OleDbConnection connection;
		OleDbCommand command;
		OleDbDataReader dr;
		String contenido_todos_los_campos;
		String contenido_todas_las_localizaciones;
		static bool campos_tematicos = true;
		protected void Page_Load(object sender, EventArgs e)
		{
			connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
			command = new OleDbCommand("SELECT id_campo_tematico,campo_tematico,id_campo_padre,nivel FROM CamposTematicos ORDER BY campo_tematico;", connection);
			connection.Open();
			int id_campo;
			String campo_tematico;
			int campo_padre;
			int nivel;
			try
			{
				dr = command.ExecuteReader();
				contenido_todos_los_campos = "<ul style = \"text-align: justify; font-size:large;\">";
				while (dr.Read())
				{
					id_campo = dr.GetInt32(0);
					campo_tematico = dr.GetString(1);
					campo_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_todos_los_campos += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo + "&id_campo_padre=" + campo_padre + "&nivel=" + nivel + "'>";
					contenido_todos_los_campos += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico);
					contenido_todos_los_campos += "</a></li>";
				}
				contenido_todos_los_campos += "</ul>";
			}
			catch { }
			todos_los_campos.Text = contenido_todos_los_campos;

			int id_loc;
			String loc;
			int loc_padre;
			command = new OleDbCommand("SELECT id_localizacion,localizacion,id_loc_padre,nivel FROM Localizaciones ORDER BY localizacion;", connection);
			try
			{
				dr = command.ExecuteReader();
				contenido_todas_las_localizaciones = "<ul style = \"text-align: justify; font-size:large;\">";
				while (dr.Read())
				{
					id_loc = dr.GetInt32(0);
					loc = dr.GetString(1);
					loc_padre = dr.GetInt32(2);
					nivel = dr.GetInt32(3);
					contenido_todas_las_localizaciones += "<li><a href='Localizaciones.aspx?word=" + loc + "&id_localizacion=" + id_loc + "&id_loc_padre=" + loc_padre + "&nivel=" + nivel + "'>" + loc + "</a></li>";
				}
				contenido_todas_las_localizaciones += "</ul>";
			}
			catch { }
			todas_las_localizaciones.Text = contenido_todas_las_localizaciones;

			if (campos_tematicos)
			{
				label_elegible.Text = "<p><b>Todos los campos temáticos</b></p>";
				columna_elegible.Text = contenido_todos_los_campos;
				Button_Change.Text = "Mostrar localizaciones";
			}
			else
			{
				label_elegible.Text = "<p><b>Todas las localizaciones</b></p>";
				columna_elegible.Text = contenido_todas_las_localizaciones;
				Button_Change.Text = "Mostrar campos temáticos";
			}

			connection.Close();
		}

		protected void Button_Lemas_Click(object sender, EventArgs e)
		{
			if (!TextBox_Lemas.Text.Trim().Equals(""))
			{
				Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Lemas.Text.Trim());
			}
		}

		protected void Button_Change_Click(object sender, EventArgs e)
		{
			if (campos_tematicos)
			{
				campos_tematicos = false;
				label_elegible.Text = "<p><b>Todas las localizaciones</b></p>";
				columna_elegible.Text = contenido_todas_las_localizaciones;
				Button_Change.Text = "Mostrar campos temáticos";
			}
			else
			{
				campos_tematicos = true;
				label_elegible.Text = "<p><b>Todos los campos temáticos</b></p>";
				columna_elegible.Text = contenido_todos_los_campos;
				Button_Change.Text = "Mostrar localizaciones";
			}
		}
	}
}
 