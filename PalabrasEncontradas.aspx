 <%@ Page Title="Diccionario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PalabrasEncontradas.aspx.cs" Inherits="MetaDiccionario.PalabrasEncontradas" %>

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
			}
	</style>

	<div>
		<div class="row">
			<div class="col-lg-1 col-md-1 hidden-sm hidden-xs">

			</div>
			<div class="col-lg-10 col-md-10 col-sm-12 col-xs-12 row">
				<div class="hidden-lg hidden-md col-sm-1 col-xs-1">

				</div>
				<div class="col-lg-12 col-md-12 col-sm-10 col-xs-10 row">
					<div class="col-lg-8 col-md-8 col-sm-6 col-xs-8">
						<br />
						<br />
						<div><p style="text-align: left;"><p><b>Introduzca un término de búsqueda</b></p></div>
						<div><asp:TextBox class="form-control" ID="TextBox_Lemas" runat="server" style="min-width: 100px; max-width:none; text-align: center;"></asp:TextBox></div>
						<br />
					</div>
					<div id="button_search" class="col-lg-4 col-md-4 col-sm-6 col-xs-4">
						<div>
							<br />
							<br />
							<br />
						</div>
						<div style="text-align: center;">
							<asp:Button ID="Button_Lemas" runat="server" Text="Buscar" class="btn btn-primary" OnClick="Button_Lemas_Click" />
						</div>
					</div>
				</div>
				<div class="hidden-lg hidden-md col-sm-1 col-xs-1">

				</div>
			</div>
			<div class="col-lg-1 col-md-1 hidden-sm hidden-xs">

			</div>
		</div>
		<br />
		<div class="hidden-xs row">
			<div class="col-lg-3 col-md-6 col-sm-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 80vh; max-height: 80vh; text-align: center; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Acepciones del diccionario</b></p>
				<asp:Label ID="lemas_encontrados" runat="server" Text="" class="container"></asp:Label>
			</div>
			<div class="col-lg-3 col-md-6 col-sm-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 80vh; max-height: 80vh; text-align: center; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Verbos</b></p>
				<asp:Label ID="verbos_encontrados" runat="server" Text="" class="container"></asp:Label>
			</div>
			<div class="col-lg-3 col-md-6 col-sm-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 80vh; max-height: 80vh; text-align: center; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Campos temáticos</b></p>
				<asp:Label ID="campos_encontrados" runat="server" Text="" class="container"></asp:Label>
			</div>
			<div class="col-lg-3 col-md-6 col-sm-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 80vh; max-height: 80vh; text-align: center; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Localizaciones</b></p>
				<asp:Label ID="localizaciones_encontradas" runat="server" Text="" class="container"></asp:Label>
			</div>
		</div>
		<div class="hidden-lg hidden-md hidden-sm">
			<div style="background-color:antiquewhite; overflow-y:auto; min-height: 80vh; max-height: 80vh; text-align: center; border-style: double;">
				<asp:Label ID="opcion_elegida" runat="server" Text="" style="text-align: center; font-size: larger;"></asp:Label>
				<asp:Label ID="columna_elegida" runat="server" Text="" class="container"></asp:Label>
			</div>
			<br />
			<div>
				<div style="text-align: justify; padding: 5vh; max-height: 50vh;">
					<asp:RadioButtonList ID="Opciones" runat="server">
						<asp:ListItem Value="0" Selected="True">&nbsp Acepciones del diccionario</asp:ListItem>
						<asp:ListItem Value="1">&nbsp Conjugaciones verbales</asp:ListItem>
						<asp:ListItem Value="2">&nbsp Campos temáticos</asp:ListItem>
						<asp:ListItem Value="3">&nbsp Localizaciones</asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<div style="text-align: center; color: antiquewhite;">
						<asp:Button ID="Change" runat="server" Text="Cambiar opción" class="btn btn-primary"/>
					</div>
				</div>
			</div>
		</div>
		<br />
	</div>
	
</asp:Content>
