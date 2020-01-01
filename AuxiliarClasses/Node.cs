using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;

namespace MetaDiccionario
{
	public class Node
	{
		private int id;
		private String nombre;
		private int id_padre;
		private int nivel;
		private List<Node> sons;
		private int tipo; //campo_tematico=0;localizacion=1

		public Node(int id, String nombre, int id_padre, int nivel,int tipo)
		{
			this.id = id;
			this.nombre = nombre;
			this.id_padre = id_padre;
			this.nivel = nivel;
			this.tipo = tipo;
			sons = new List<Node>();
		}

		public int GetId()
		{
			return id;
		}

		public String GetNombre()
		{
			return nombre;
		}

		public int GetIdLocPadre()
		{
			return id_padre;
		}

		public int GetNivel()
		{
			return nivel;
		}

		public List<Node> GetSons()
		{
			return sons;
		}

		public Node AddSon(int id_son,String name,int id_padre,int nivel)
		{
			Node new_son = new Node(id_son,name,id_padre,nivel,this.tipo);
			sons.Add(new_son);
			return new_son;
		}

		public String PrintNode()
		{
			if (id != 0)
			{
				String result = "";
				for (int i = 0; i < nivel; i++)
				{
					result += "&nbsp&nbsp&nbsp&nbsp";
				}
				if (tipo == 1)
				{
					result += "<li><a href='LocalizacionesEncontradas.aspx?word=" + nombre + "&id_localizacion=" + id + "&id_loc_padre=" + id_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombre) + "</a></li>";
				}
				else
				{
					result += "<li><a href='CamposTematicos.aspx?word=" + nombre + "&id_campo_tematico=" + id + "&id_campo_padre=" + id_padre + "&nivel=" + nivel + "'>" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombre) + "</a></li>";
				}
				for (int i = 0; i < sons.Count; i++)
				{
					result += sons[i].PrintNode();
				}
				return result;
			}
			else
			{
				return "";
			}
			
		}
	}
}