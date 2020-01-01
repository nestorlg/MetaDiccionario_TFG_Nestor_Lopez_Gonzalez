<%@ Page Title="Diccionario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcepcionElegida.aspx.cs" Inherits="MetaDiccionario.AcepcionElegida" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<style type="text/css">
			@media screen {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 9px;
				}
				
				#contenido-acepcion {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#acepciones-del-lema {
					min-height: 60vh;
					max-height: 60vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 40vh;
					text-align: justify;
					padding: 0vh;
					overflow-y: auto;
				}
			}

			@media screen and (min-width: 400px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 11px;
				}
				
				#contenido-acepcion {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#acepciones-del-lema {
					min-height: 60vh;
					max-height: 60vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 40vh;
					text-align: justify;
					padding: 0vh;
					overflow-y: auto;
				}
			}

			@media screen and (min-width: 558px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}
				
				#contenido-acepcion {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#acepciones-del-lema {
					min-height: 75vh;
					max-height: 75vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 40vh;
					text-align: justify;
					padding: 0vh;
					overflow-y: auto;
				}
			}

			@media screen and (min-width: 768px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}
				
				#contenido-acepcion {
					max-height: 40vh;
					overflow-y:auto;
				}

				#acepciones-del-lema {
					min-height: 75vh;
					max-height: 75vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 40vh;
					text-align: justify;
					padding: 0vh;
					font-size: 12px;
				}
			}

			@media screen and (min-width: 1150px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}
				
				#contenido-acepcion {
					max-height: 75vh;
					overflow-y: auto;
				}

				#acepciones-del-lema {
					min-height: 75vh;
					max-height: 75vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 75vh;
					text-align: justify;
					padding: 0vh;
					font-size: 12px;
				}
			}

			@media screen and (min-width: 1300px) {
				.jumbotron {
					margin-top: 0px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}
				
				#contenido-acepcion {
					min-height: 75vh;
					max-height: 75vh;
					overflow-y: auto;
				}

				#acepciones-del-lema {
					min-height: 75vh;
					max-height: 75vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					max-height: 75vh;
					text-align: justify;
					padding: 0vh;
					font-size: 12px;
				}
			}
	</style>

	<div class="row">
        <div id="acepciones-del-lema" class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
			<asp:Label ID="acepciones_del_lema" runat="server" Text="" class="container">
			</asp:Label>
        </div>
        <div id="contenido-acepcion" class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
			<br />
			<div class="center-block" style="text-align: center;">
				<div><p style="text-align: left;"><b>Introduzca un término de búsqueda</b></p></div>
				<div><asp:TextBox class="form-control" ID="TextBox_Lemas" runat="server" style="min-width: 100px; max-width:none; text-align: center;"></asp:TextBox></div>
				<br />
				<div style="text-align: center; color: antiquewhite;">
					<asp:Button ID="Button_Lemas" runat="server" Text="Buscar" class="btn btn-primary" OnClick="Button_Lemas_Click" />
				</div>
			</div>
			<br />
			<asp:Label ID="lema_elegido" runat="server" Text="" class="container" style="text-align: justify;"></asp:Label>
			<div style="padding: 5vh; min-height: 50vh; max-height: 50vh;">
				<asp:Label ID="acepcion_elegida" runat="server" Text="" class="container" style="text-align: center;"></asp:Label>
				<br />
				<div style="text-align: center; color: antiquewhite;">
					<asp:Button ID="Mas_Informacion" runat="server" Text="Más" class="btn btn-primary " OnClick="Mas_Informacion_Click" />
				</div>
				<div>
					<asp:Label ID="informacion_extra" runat="server" Text="" class="container" style="text-align: center;"></asp:Label>
				</div>
			</div>
        </div>
        <div id="radio-button" class="col-lg-3 col-md-3 col-sm-4 col-xs-12 row">
			<div class="col-lg-1 col-md-1 hidden-sm hiden-xs">

			</div>
			<div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
				<br />
				<asp:RadioButtonList ID="Opciones" runat="server">
					<asp:ListItem Value="0" Selected="True">&nbsp Definición general</asp:ListItem>
					<asp:ListItem Value="1">&nbsp Sinónimos</asp:ListItem>
					<asp:ListItem Value="2">&nbsp Formas flexionadas</asp:ListItem>
					<asp:ListItem Value="3">&nbsp Palabras derivadas</asp:ListItem>
					<asp:ListItem Value="4">&nbsp Dudas lingüísticas</asp:ListItem>
					<asp:ListItem Value="5">&nbsp Locuciones y colocaciones</asp:ListItem>
					<asp:ListItem Value="6">&nbsp Campos temáticos</asp:ListItem>
					<asp:ListItem Value="7">&nbsp Preposiciones</asp:ListItem>
				</asp:RadioButtonList>
				<br />
				<div style="color: antiquewhite;">
					<asp:Button ID="Change_Option" runat="server" Text="Buscar" class="btn btn-primary"/>
				</div>
				<br />
				<asp:Image id="Imagen" runat="server" ImageUrl="" Height="190px" Width="190px" AlternateText="Imagen no disponible" ImageAlign="TextTop" />
			</div>
			<div class="col-lg-1 col-md-1 hidden-sm hiden-xs">

			</div>
        </div>
    </div>

</asp:Content>
