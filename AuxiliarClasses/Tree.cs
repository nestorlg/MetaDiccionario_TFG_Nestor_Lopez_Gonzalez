using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaDiccionario
{
	public class Tree
	{
		private Node root;
		private List<Node> nodes;
		private int tipo; //campo_tematico=0;localizacion=1

		public Tree (int id_root,int tipo)
		{
			root = new Node(id_root,"",0,0,tipo);
			nodes = new List<Node>();
			nodes.Add(root);
			this.tipo = tipo;
		}

		public void AddNode(Node nodo)
		{
			nodes.Add(nodo);
		}

		public String PrintTree ()
		{
			String result = "<ul>";
			List<Node> sons = root.GetSons();
			for (int i = 0;i < sons.Count; i++)
			{
				result += sons[i].PrintNode();
			}
			return result + "</ul>";
		}

		public void AddSonToRoot(int id,String name,int id_padre,int nivel)
		{
			nodes.Add(root.AddSon(id, name, id_padre, nivel));
		}

		public Node GetSon(int id)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes[i].GetId() == id) return nodes[i];
			}
			return null;
		}
	}
}