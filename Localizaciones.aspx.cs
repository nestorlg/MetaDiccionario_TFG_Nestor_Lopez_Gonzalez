using System;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class Localizaciones : System.Web.UI.Page
	{
		OleDbConnection connection;
		OleDbCommand command;
		OleDbDataReader dr;
		String contenido_localizacion_elegida;
		String contenido_todas_las_localizaciones;
		int id_loc;
		String loc;
		String word;
		int id_localizacion;
		int id_loc_padre;
		int nivel;
		static bool alpha = true;
		protected void Page_Load(object sender, EventArgs e)
		{
			word = Request.QueryString["word"];
			id_localizacion = Convert.ToInt32(Request.QueryString["id_localizacion"]);
			id_loc_padre = Convert.ToInt32(Request.QueryString["id_loc_padre"]);
			nivel = Convert.ToInt32(Request.QueryString["nivel"]);
			connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
			connection.Open();
			contenido_localizacion_elegida = "";
			contenido_localizacion_elegida += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word) + "</b>:<br/><br/><br/><ul>";
			contenido_localizacion_elegida += AuxiliarTools.Acepciones_de_localizaciones_hijas(id_localizacion, connection);
			contenido_localizacion_elegida += "</u></h1>";
			localizacion_elegida.Text = contenido_localizacion_elegida;

			if (alpha)
			{
				command = new OleDbCommand("SELECT id_localizacion,localizacion,id_loc_padre,nivel FROM Localizaciones ORDER BY localizacion;", connection);
				try
				{
					dr = command.ExecuteReader();
					contenido_todas_las_localizaciones = "<ul style = \"text-align: justify; font-size:large;\">";
					while (dr.Read())
					{
						id_loc = dr.GetInt32(0);
						loc = dr.GetString(1);
						id_loc_padre = dr.GetInt32(2);
						nivel = dr.GetInt32(3);
						contenido_todas_las_localizaciones += "<li><a href='Localizaciones.aspx?word=" + loc + "&id_localizacion=" + id_loc + "&id_loc_padre=" + id_loc_padre + "&nivel=" + nivel + "'>";
						if (id_loc == id_localizacion)
						{
							contenido_todas_las_localizaciones += "<b>" + loc +"</b>";
						}
						else
						{
							contenido_todas_las_localizaciones += loc;
						}
						contenido_todas_las_localizaciones += "</a></li>";
					}
					contenido_todas_las_localizaciones += "</ul>";
				}
				catch { }
			}
			else
			{
				contenido_todas_las_localizaciones = "<ul style = \"text-align: justify; font-size:large;\">";
				contenido_todas_las_localizaciones += AuxiliarTools.Localizaciones_orden_jerarquico(0, connection,0,id_localizacion);
				contenido_todas_las_localizaciones += "</ul>";
			}

			todas_las_localizaciones.Text = contenido_todas_las_localizaciones;
			connection.Close();
		}

		protected void Button_Change_Click(object sender, EventArgs e)
		{
			connection.Open();
			if (alpha)
			{
				alpha = false;
				Button_Change.Text = "Orden alfabético";
				contenido_todas_las_localizaciones = "<ul style = \"text-align: justify; font-size:large;\">";
				contenido_todas_las_localizaciones += AuxiliarTools.Localizaciones_orden_jerarquico(0, connection, 0, id_localizacion);
				contenido_todas_las_localizaciones += "</ul>";
			}
			else
			{
				alpha = true;
				Button_Change.Text = "Orden jerárquico";
				command = new OleDbCommand("SELECT id_localizacion,localizacion,id_loc_padre,nivel FROM Localizaciones ORDER BY localizacion;", connection);
				try
				{
					dr = command.ExecuteReader();
					contenido_todas_las_localizaciones = "<ul style = \"text-align: justify; font-size:large;\">";
					while (dr.Read())
					{
						id_localizacion = dr.GetInt32(0);
						loc = dr.GetString(1);
						id_loc_padre = dr.GetInt32(2);
						nivel = dr.GetInt32(3);
						contenido_todas_las_localizaciones += "<li><a href='Localizaciones.aspx?word=" + loc + "&id_localizacion=" + id_loc + "&id_loc_padre=" + id_loc_padre + "&nivel=" + nivel + "'>";
						if (id_loc == id_localizacion)
						{
							contenido_todas_las_localizaciones += "<b>" + loc + "</b>";
						}
						else
						{
							contenido_todas_las_localizaciones += loc;
						}
						contenido_todas_las_localizaciones += "</a></li>";
					}
					contenido_todas_las_localizaciones += "</ul>";
				}
				catch { }
			}
			todas_las_localizaciones.Text = contenido_todas_las_localizaciones;
			connection.Close();
		}

		protected void Button_Localizaciones_Click(object sender, EventArgs e)
		{
			if (!TextBox_Localizaciones.Text.Trim().Equals(""))
			{
				Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Localizaciones.Text.Trim());
			}
		}
	}
}