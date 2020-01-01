using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace MetaDiccionario
{
	public partial class AcepcionesDelLema : System.Web.UI.Page
	{
		OleDbConnection connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbCommand command;
		String contenido_acepciones_de_un_lema;
		protected void Page_Load(object sender, EventArgs e)
		{
			String word = Request.QueryString["word"];
			int id_lema = Convert.ToInt32(Request.QueryString["lema"]);
			command = new OleDbCommand("SELECT id_acepcion,NumAcepcion FROM Acepciones WHERE id_lema = " + id_lema + ";", connection);
			try
			{
				connection.Open();
				OleDbDataReader dr = command.ExecuteReader();
				contenido_acepciones_de_un_lema = "";
				contenido_acepciones_de_un_lema += "<h1 style = \"text-align: justify; font-size:x-large;\">" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word) + "</h1><br /><br /><ul style = \"text-align: justify; font-size:large;\">";
				int id_acepcion = 0;
				int num_acepcion;
				int count = 0;
				while (dr.Read())
				{
					id_acepcion = dr.GetInt32(0);
					num_acepcion = dr.GetInt32(1);
					command = new OleDbCommand("SELECT significado FROM Significados WHERE id_significado IN (SELECT id_significado FROM Acepcion_Significado WHERE id_acepcion = " + id_acepcion + ");", connection);
					try
					{
						OleDbDataReader dr_x = command.ExecuteReader();
						if (dr_x.Read())
						{
							String significado = dr_x.GetString(0).ToString();
							contenido_acepciones_de_un_lema += "<li><a href='AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + word + "'>" + num_acepcion + "</a>) " + significado + "</li>";
						}
						count++;
					}
					catch (Exception exc)
					{
						contenido_acepciones_de_un_lema = "";
						contenido_acepciones_de_un_lema += "<h1 style = \"text-align: justify; font-size:large;\">";
						contenido_acepciones_de_un_lema += exc.ToString();
						contenido_acepciones_de_un_lema += "</h1>";
						contenido_acepciones_de_un_lema += "</ul>";
						acepciones_de_un_lema.Text = contenido_acepciones_de_un_lema;
					}
				}
				if (count == 1)
				{
					Response.Redirect("AcepcionElegida.aspx?acepcion=" + id_acepcion + "&lema=" + word);
				}
			}
			catch (Exception exc)
			{
				contenido_acepciones_de_un_lema = "";
				contenido_acepciones_de_un_lema += "<h1 style = \"text-align: justify; font-size:large;\">";
				contenido_acepciones_de_un_lema += exc.ToString();
				contenido_acepciones_de_un_lema += "</h1>";
			}
			finally
			{
				acepciones_de_un_lema.Text = contenido_acepciones_de_un_lema;
				connection.Close();
			}
		}

		protected void Button_Lemas_Click(object sender, EventArgs e)
		{
			Response.Redirect("LemasEncontrados.aspx?word=" + TextBox_Lemas.Text.Trim() + "&lema=1");
		}
	}
}