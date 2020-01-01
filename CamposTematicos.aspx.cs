using System;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class CamposTematicos : System.Web.UI.Page
	{
		OleDbConnection connection;
		OleDbCommand command;
		OleDbDataReader dr;
		String contenido_campo_elegido;
		String contenido_todos_los_campos;
		int id_campo;
		String campo_tematico;
		static bool alpha = true;
		String word;
		int id_campo_tematico;
		int id_campo_padre;
		int nivel;
		protected void Page_Load(object sender, EventArgs e)
		{
			word = Request.QueryString["word"];
			id_campo_tematico = Convert.ToInt32(Request.QueryString["id_campo_tematico"]);
			id_campo_padre = Convert.ToInt32(Request.QueryString["id_campo_padre"]);
			nivel = Convert.ToInt32(Request.QueryString["nivel"]);
			connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
			connection.Open();
			contenido_campo_elegido = "";
			contenido_campo_elegido += "<h1 style = \"text-align: justify; font-size:large;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word) + "</b>:<br/><br/><br/><ul>";
			contenido_campo_elegido += AuxiliarTools.Acepciones_de_campos_hijos(id_campo_tematico,connection);
			contenido_campo_elegido += "</ul></h1>";
			campo_elegido.Text = contenido_campo_elegido;

			if (alpha)
			{
				command = new OleDbCommand("SELECT id_campo_tematico,campo_tematico,id_campo_padre,nivel FROM CamposTematicos ORDER BY campo_tematico;", connection);
				try
				{
					dr = command.ExecuteReader();
					contenido_todos_los_campos = "<ul style = \"text-align: justify; font-size:large;\">";
					while (dr.Read())
					{
						id_campo = dr.GetInt32(0);
						campo_tematico = dr.GetString(1);
						id_campo_padre = dr.GetInt32(2);
						nivel = dr.GetInt32(3);
						contenido_todos_los_campos += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo + "&id_campo_padre=" + id_campo_padre + "&nivel=" + nivel + "'>";
						if (id_campo == id_campo_tematico)
						{
							contenido_todos_los_campos += "<b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico) + "</b>";
						}
						else
						{
							contenido_todos_los_campos += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico);
						}
						contenido_todos_los_campos += "</a></li>";
					}
					contenido_todos_los_campos += "</ul>";
				}
				catch { }
			}
			else
			{
				contenido_todos_los_campos = "<ul style = \"text-align: justify; font-size:large;\">";
				contenido_todos_los_campos += AuxiliarTools.Campos_orden_jerarquico(0, connection,0, id_campo_tematico);
				contenido_todos_los_campos += "</ul>";
			}
			
			todos_los_campos.Text = contenido_todos_los_campos;
			connection.Close();
		}

		protected void Button_Change_Click(object sender, EventArgs e)
		{
			connection.Open();
			if (alpha)
			{
				alpha = false;
				Button_Change.Text = "Orden alfabético";
				contenido_todos_los_campos = "<ul style = \"text-align: justify; font-size:large;\">";
				contenido_todos_los_campos += AuxiliarTools.Campos_orden_jerarquico(0, connection, 0, id_campo_tematico);
				contenido_todos_los_campos += "</ul>";
			}
			else
			{
				alpha = true;
				Button_Change.Text = "Orden jerárquico";
				command = new OleDbCommand("SELECT id_campo_tematico,campo_tematico,id_campo_padre,nivel FROM CamposTematicos ORDER BY campo_tematico;", connection);
				try
				{
					dr = command.ExecuteReader();
					contenido_todos_los_campos = "<ul style = \"text-align: justify; font-size:large;\">";
					while (dr.Read())
					{
						id_campo = dr.GetInt32(0);
						campo_tematico = dr.GetString(1);
						id_campo_padre = dr.GetInt32(2);
						nivel = dr.GetInt32(3);
						contenido_todos_los_campos += "<li><a href='CamposTematicos.aspx?word=" + campo_tematico + "&id_campo_tematico=" + id_campo + "&id_campo_padre=" + id_campo_padre + "&nivel=" + nivel + "'>";
						if (id_campo == id_campo_tematico)
						{
							contenido_todos_los_campos += "<b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico) + "</b>";
						}
						else
						{
							contenido_todos_los_campos += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(campo_tematico);
						}
						contenido_todos_los_campos += "</a></li>";
					}
					contenido_todos_los_campos += "</ul>";
				}
				catch { }
			}
			todos_los_campos.Text = contenido_todos_los_campos;
			connection.Close();
		}

		protected void Button_Campos_Click(object sender, EventArgs e)
		{
			Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Campos.Text.Trim());
		}
	}
}