<%@ Page Title="Diccionario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MetaDiccionario._Default" %>

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
					font-size: larger;
				}

				.hidden-xxs {
					font-size: 14px;
				}
			}
	</style>

	<div>
		<h1 class="center-block" style="text-align: center;">Diccionario</h1>
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
						<div><p style="text-align: left;"><b>Introduzca un término de búsqueda</b></p></div>
						<div><asp:TextBox class="form-control container" ID="TextBox_Lemas" runat="server" style="min-width: 100px; max-width:none; text-align:center;"></asp:TextBox></div>
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
		<br />
		<div class="hidden-xs row">
			<div class="col-lg-1 col-md-1 hidden-sm">

			</div>
			<div class="col-lg-4 col-md-4 col-sm-6 col-xs-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 65vh; max-height: 65vh; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Todos los campos temáticos</b></p>
				<asp:Label ID="todos_los_campos" runat="server" Text="" class="container columna-campos"></asp:Label>
			</div>
			<div class="col-lg-2 col-md-2 hidden-sm">
				
			</div>
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6" style="background-color:antiquewhite; overflow-y:auto; min-height: 65vh; max-height: 65vh; border-style: double;">
				<p style="text-align: center; font-size: larger;"><b>Todas las localizaciones</b></p>
				<asp:Label ID="todas_las_localizaciones" runat="server" Text="" class="container columna-localizaciones"></asp:Label>
			</div>
			<div class="col-lg-1 col-md-1 hidden-sm">

			</div>
		</div>
		<div class="hidden-lg hidden-md hidden-sm">
			<div style="background-color:antiquewhite; overflow-y:auto; min-height: 65vh; max-height: 65vh; border-style: double;">
				<asp:Label ID="label_elegible" runat="server" Text="" class="container" style="text-align: center; font-size: larger;"></asp:Label>
				<asp:Label ID="columna_elegible" runat="server" Text="" class="container"></asp:Label>
			</div>
			<br />
			<div style="text-align: center; color: antiquewhite;">
				<asp:Button ID="Button_Change" runat="server" Text="Mostrar localizaciones" class="btn btn-primary" OnClick="Button_Change_Click" />
			</div>
		</div>
		<br />
	</div>

</asp:Content>