using System;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public partial class ConjugacionesVerbales : System.Web.UI.Page
	{
		OleDbConnection connection;
		OleDbCommand command;
		OleDbCommand command_all_flex;
		OleDbDataReader dr;
		OleDbDataReader dr_all_flex;
		String contenido_verbos_encontrados;
		String contenido_verbo_elegido;
		String persona = "Genérica";
		int id_numero = 3;
		String not_equal = "";
		int opcion;
		static bool firstTime = true;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (verbos_encontrados.Text == "")
			{
				firstTime = true;
			}
			String word = Request.QueryString["word"];
			int id = Convert.ToInt32(Request.QueryString["lema"]);
			connection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);

			opcion = Convert.ToInt32(Opciones.SelectedValue);

			switch (Opciones.SelectedIndex)
			{
				case 0:
					persona = "Genérica";
					id_numero = 3;
					not_equal = "";
					break;
				case 1:
					persona = "1ª persona";
					id_numero = 1;
					not_equal = " NOT";
					break;
				case 2:
					persona = "2ª persona (informal)";
					id_numero = 1;
					not_equal = " NOT";
					break;
				case 3:
					persona = "2ª persona (formal)";
					id_numero = 1;
					not_equal = " NOT";
					break;
				case 4:
					persona = "2ª persona (voseo)";
					id_numero = 1;
					not_equal = " NOT";
					break;
				case 5:
					persona = "3ª persona";
					id_numero = 1;
					not_equal = " NOT";
					break;
				case 6:
					persona = "1ª persona";
					id_numero = 2;
					not_equal = " NOT";
					break;
				case 7:
					persona = "2ª persona (informal)";
					id_numero = 2;
					not_equal = " NOT";
					break;
				case 8:
					persona = "2ª persona (formal)";
					id_numero = 2;
					not_equal = " NOT";
					break;
				case 9:
					persona = "3ª persona";
					id_numero = 2;
					not_equal = " NOT";
					break;
				default:
					break;
			}

			Opciones.Visible = true;
			Change.Visible = true;
			command_all_flex = new OleDbCommand("SELECT id_flexion,flexión,abreviatura,modo FROM Flexiones WHERE persona='" + persona + "' AND id_numero=" + id_numero + " AND" + not_equal + " modo='No personal';", connection);
			try
			{
				connection.Open();
				dr_all_flex = command_all_flex.ExecuteReader();
				contenido_verbo_elegido = "";
				int id_flexion;
				String flexion;
				String abreviatura;
				String forma;
				String modo;
				String modo_anterior = "";
				while (dr_all_flex.Read())
				{
					id_flexion = dr_all_flex.GetInt32(0);
					flexion = dr_all_flex.GetString(1);
					abreviatura = dr_all_flex.GetString(2);
					modo = dr_all_flex.GetString(3);

					if (!modo.Equals(modo_anterior))
					{
						if (!modo_anterior.Equals(""))
						{
							contenido_verbo_elegido += "</tbody></table>";
						}
						contenido_verbo_elegido += "<br /><br /><h1 style = \"text-align: justify; font-size:large;\">" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modo) + "</h1><br /><br /><table class='table table-striped'><thead><tr><th>Tiempo</th><th>Abreviatura</th><th>Forma verbal</th></tr></thead><tbody>";
					}

					command = new OleDbCommand("SELECT palabra FROM Acepcion_Flexion WHERE id_flexion=" + id_flexion + " AND id_acepcion IN (SELECT TOP 1 id_acepcion FROM Acepciones WHERE id_lema=" + id + ");", connection);
					dr = command.ExecuteReader();

					if (dr.Read())
					{
						forma = dr.GetString(0);

						contenido_verbo_elegido += "<tr><td>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(flexion) + "</td><td>" + abreviatura + "</td><td>" + forma + "</td></tr>";
					}
					else
					{
						contenido_verbo_elegido += "<tr><td>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(flexion) + "</td><td>" + abreviatura + "</td><td>-</td></tr>";
					}

					modo_anterior = modo;
				}
				contenido_verbo_elegido += "</tbody></table>";
			}
			catch { }
			lema_elegido.Text = "<h1 style = \"text-align: center; font-size:24pt;\"><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word) + "</b></h1>";
			verbo_elegido.Text = contenido_verbo_elegido;
			if (firstTime)
			{
				command = new OleDbCommand("SELECT id_lema,lema,etimología FROM Lemas WHERE id_lema IN (SELECT id_lema FROM Acepciones WHERE id_categoria=3) ORDER BY lema;", connection);
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
						if (id_lema == id)
						{
							contenido_verbos_encontrados += "<li><a href='ConjugacionesVerbales.aspx?word=" + lema + "&lema=" + id_lema + "'><b>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")" + "</b></a></li>";
						}
						else
						{
							contenido_verbos_encontrados += "<li><a href='ConjugacionesVerbales.aspx?word=" + lema + "&lema=" + id_lema + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lema) + " (" + etimologia + ")" + "</a></li>";
						}
					}
					contenido_verbos_encontrados += "</ul>";
					verbos_encontrados.Text = contenido_verbos_encontrados;
				}
				catch (Exception exc)
				{
					verbos_encontrados.Text = exc.ToString();
				}
			}
			firstTime = false;
			connection.Close();
		}

		protected void Button_Verbos_Click(object sender, EventArgs e)
		{
			Response.Redirect("PalabrasEncontradas.aspx?word=" + TextBox_Verbos.Text.Trim());
		}
	}
}