<%@ Page Title="Localizaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Localizaciones.aspx.cs" Inherits="MetaDiccionario.Localizaciones" %>

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

				#otras-localizaciones {
					min-height: 50vh;
					max-height: 50vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
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

				#otras-localizaciones {
					min-height: 50vh;
					max-height: 50vh;
					max-width: none;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}
			}

			@media screen and (min-width: 992px) {
				.jumbotron {
					margin-top: 20px;
				}

				.body-content {
					padding: 0;
				}

				.hidden-xxs {
					font-size: 14px;
				}

				#otras-localizaciones {
					min-height: 83vh;
					max-height: 83vh;
					max-width: 45vh;
					background-color: antiquewhite;
					overflow-y: auto;
					border-style: double;
				}
			}
	</style>

    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
			<div id="otras-localizaciones">
				<asp:Label ID="todas_las_localizaciones" runat="server" Text="" class="container">
				</asp:Label>
			</div>
			<br />
			<div style="text-align: center; color: antiquewhite;">
				<asp:Button ID="Button_Change" runat="server" Text="Orden jerárquico" class="btn btn-primary" OnClick="Button_Change_Click" />
			</div>
        </div>
        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" style="padding: 5vh;">
			<br />
			<br />
			<h1 class="center-block" style="text-align: center;">Localizaciones</h1>
			<br />
			<br />
			<div class="center-block" style="text-align: center;">
				<div><p style="text-align: left;"><b>Introduzca un término de búsqueda</b></p></div>
				<div><asp:TextBox class="form-control" ID="TextBox_Localizaciones" runat="server" style="min-width: 100px; max-width:none; text-align:center;"></asp:TextBox></div>
				<br />
				<div style="text-align: center; color: antiquewhite;">
					<asp:Button ID="Button_Localizaciones" runat="server" Text="Buscar" class="btn btn-primary" OnClick="Button_Localizaciones_Click" />
				</div>
			</div>
			<br />
			<br />
			<div style="max-height: 50vh; overflow-y:auto;">
				<asp:Label ID="localizacion_elegida" runat="server" Text="" class="container">
				</asp:Label>
			</div>
			<br />
        </div>
    </div>

</asp:Content>
