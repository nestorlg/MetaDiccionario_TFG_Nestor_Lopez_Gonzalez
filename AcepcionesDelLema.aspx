<%@ Page Title="Diccionario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcepcionesDelLema.aspx.cs" Inherits="MetaDiccionario.AcepcionesDelLema" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<style type="text/css">

	</style>

	<script type="text/javascript">
		$(document).ready(function () {
			$('.navbar .nav').find('.active').removeClass('active');
			$('.navbar .nav li a').each(function () {
				var url = this.href;
				if (url.includes('Default')) {
					$(this).parent().addClass('active');
				}
			});
		});
	</script>

    <div class="row">
        <div class="col-md-3">
			
        </div>
        <div class="col-md-6">
			<br />
			<br />
			<h1 class="center-block" style="width:30%; text-align:center;">Diccionario</h1>
			<br />
			<br />
			<form class="container">
				<div class="center-block" style="width:30%; text-align: center;">
					<div><label>Introduzca un término de búsqueda</label></div>
					<div>
						<asp:TextBox class="form-control" ID="TextBox_Lemas" runat="server"></asp:TextBox>
					</div>
					<br />
					<asp:Button ID="Button_Lemas" runat="server" Text="Buscar" class="btn btn-primary" OnClick="Button_Lemas_Click" />
				</div>
			</form>
			<br />
			<br />
			<asp:Label ID="acepciones_de_un_lema" runat="server" Text="" class="container">
			</asp:Label>
        </div>
        <div class="col-md-3">
            
        </div>
    </div>

</asp:Content>
