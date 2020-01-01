<%@ Page Title="Conjugaciones Verbales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConjugacionesVerbales.aspx.cs" Inherits="MetaDiccionario.ConjugacionesVerbales" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<style type="text/css">@media screen {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 9px;
				}
				
				#contenido-verbo {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 60vh;
					max-height: 60vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 40vh;
					max-height: 40vh;
					text-align: justify;
					padding: 5vh;
					overflow-y:auto;
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
				
				#contenido-verbo {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 60vh;
					max-height: 60vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 40vh;
					max-height: 40vh;
					text-align: justify;
					padding: 5vh;
					overflow-y:auto;
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
				
				#contenido-verbo {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 60vh;
					max-height: 60vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 40vh;
					max-height: 40vh;
					text-align: justify;
					padding: 5vh;
					overflow-y:auto;
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
				
				#contenido-verbo {
					min-height: 40vh;
					max-height: 40vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 60vh;
					max-height: 60vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 40vh;
					max-height: 40vh;
					text-align: justify;
					padding: 3vh;
					font-size: 12px;
					overflow-y:auto;
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
				
				#contenido-verbo {
					min-height: 75vh;
					max-height: 75vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 75vh;
					max-height: 75vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 75vh;
					max-height: 75vh;
					text-align: justify;
					padding: 4vh;
					font-size: 12px;
					overflow-y:auto;
				}
			}

			@media screen and (min-width: 1300px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}
				
				#contenido-verbo {
					min-height: 75vh;
					max-height: 75vh;
					overflow-y:auto;
				}

				#otros-verbos {
					min-height: 75vh;
					max-height: 75vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}

				#radio-button {
					min-height: 75vh;
					max-height: 75vh;
					text-align: justify;
					padding: 5vh;
					font-size: 12px;
					overflow-y:auto;
				}
			}
	</style>

    <div class="row">
        <div id="otros-verbos" class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
			<asp:Label ID="verbos_encontrados" runat="server" Text="" class="container">
			</asp:Label>
        </div>
        <div id="contenido-verbo" class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
			<br />
			<br />
			<h1 class="center-block" style="text-align: center;">Conjugaciones Verbales</h1>
			<br />
			<br />
			<div class="center-block" style="text-align: center;">
				<div><p style="text-align: left;"><b>Introduzca un término de búsqueda</b></p></div>
				<div><asp:TextBox class="form-control" ID="TextBox_Verbos" runat="server" style="min-width: 100px; max-width:none; align-content:center;"></asp:TextBox></div>
				<br />
				<div style="text-align: center; color: antiquewhite;">
					<asp:Button ID="Button_Verbos" runat="server" Text="Buscar" class="btn btn-primary" OnClick="Button_Verbos_Click" />
				</div>
			</div>
			<br />
			<br />
			<asp:Label ID="lema_elegido" runat="server" Text="" class="container" style="text-align: justify;"></asp:Label>
			<div style="padding: 5vh; min-height: 50vh; max-height: 50vh;">
				<asp:Label ID="verbo_elegido" runat="server" Text="" class="container">
				</asp:Label>
			</div>
        </div>
        <div id="radio-button" class="col-lg-3 col-md-3 col-sm-4 col-xs-12 row">
			<div class="col-lg-1 col-md-1 hidden-sm hiden-xs">

			</div>
			<div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
        		<asp:RadioButtonList ID="Opciones" runat="server">
					<asp:ListItem Selected="True" Value="0">&nbsp Formas no personales</asp:ListItem>
					<asp:ListItem Value="1">&nbsp Primera persona del singular</asp:ListItem>
					<asp:ListItem Value="2">&nbsp Segunda persona del singular (informal)</asp:ListItem>
					<asp:ListItem Value="3">&nbsp Segunda persona del singular (formal)</asp:ListItem>
					<asp:ListItem Value="4">&nbsp Segunda persona del singular (voseo)</asp:ListItem>
					<asp:ListItem Value="5">&nbsp Tercera persona del singular</asp:ListItem>
					<asp:ListItem Value="6">&nbsp Primera persona del plural</asp:ListItem>
					<asp:ListItem Value="7">&nbsp Segunda persona del plural (informal)</asp:ListItem>
					<asp:ListItem Value="8">&nbsp Segunda persona del plural (formal)</asp:ListItem>
					<asp:ListItem Value="9">&nbsp Tercera persona del plural</asp:ListItem>
				</asp:RadioButtonList>
				<br />
				<div style="color: antiquewhite;">
					<asp:Button ID="Change" runat="server" Text="Cambiar opción" class="btn btn-primary"/>
				</div>

			</div>
			<div class="col-lg-1 col-md-1 hidden-sm hiden-xs">

			</div>
        </div>
    </div>

</asp:Content>
