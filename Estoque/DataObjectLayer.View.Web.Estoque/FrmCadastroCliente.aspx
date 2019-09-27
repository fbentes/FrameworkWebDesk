<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCadastroCliente.aspx.cs" Inherits="DataObjectLayer.View.Web.Estoque.FrmCadastroCliente" %>
<%@ Register Assembly="DataObjectLayer.View.Web" Namespace="DataObjectLayer.View.Web" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table runat="server" id="table" class="dsds">
          <tr>
              <td colspan="3">
                <asp:Label ID="lblTitutlo"  runat="server" Text="Cadastro de Clientes" Font-Bold="true" Font-Size="Medium"></asp:Label>
              </td>
          </tr>
          <tr>
             <td  style="width:15px; height: 28px;">
                <cc2:LabelEntity ID="lblNome" runat="server" Text="Nome:" Font-Size="Small" EntitySource="Cliente"></cc2:LabelEntity>
             </td>
             <td  colspan="3" style="width:19%; height: 28px;">
                <cc2:TextBoxEntity ID="txtNome" runat="server" Width="508px"  Font-Size="Small" EntityProperty="Nome" EntitySource="Cliente"></cc2:TextBoxEntity>
               
             </td>
          </tr>
          <tr>
             <td style="width:15px">
                <cc2:LabelEntity ID="lblCpf"  Text = "CPF: " runat="server" Font-Size="Small" EntitySource="Cliente"></cc2:LabelEntity>
             </td>
             <td colspan="1" style="width:157px">
                 <cc2:TextBoxEntity ID="txtCpf" runat="server"  Width="100px" Font-Size="Small" Mask="999.999.999-99" EntityProperty="Cpf" EntitySource="Cliente" ></cc2:TextBoxEntity>
              </td>
          </tr>     
          <tr>
             <td style="width:29px; height: 28px;">
                <cc2:LabelEntity ID="lblRg" runat="server" Text="RG:" Font-Size="Small" EntityProperty="Rg" EntitySource="Cliente" ></cc2:LabelEntity>
             </td>
             <td style="width:217px; height: 28px;">
                <cc2:TextBoxEntity ID="txtRg" runat="server" Width="113px" Font-Size="Small"  Mask="99999999-9" EntityProperty="Rg" EntitySource="Cliente" IsSetEntityFromControl="True"></cc2:TextBoxEntity>
              </td>
          </tr>
          <tr>
              <td style="width:11%; height: 17px;">
                <cc2:LabelEntity ID="lblSexo" runat="server" Text="Sexo:" Font-Size="Small" EntitySource="Cliente" style=""></cc2:LabelEntity>               
              </td>
              <td style="width:50%; height: 17px;">
                <cc2:DropDownListCustom ID="ddlSexo" runat="server" Width="122px" Font-Size="Small" EntityProperty="Sexo" EntitySource="Cliente" style="left: 14px; top: 0px;" ListNamespaceSource="DataObjectLayer.Business" ListSource="SexoCollection"></cc2:DropDownListCustom>
              </td>
          </tr>
        <tr>
             <td style="width:13%; height: 26px;">
                 <cc2:LabelEntity ID="lblEndereco" runat="server" Text="Endereço:" Font-Size="Small" EntitySource="Cliente" ></cc2:LabelEntity>
              </td>
              <td style="width:41%; height: 26px;">
                 <cc2:TextBoxEntity ID="txtEndereco" runat="server" Width="347px"  Font-Size="Small" EntityProperty="Endereco" EntitySource="Cliente"></cc2:TextBoxEntity>
              </td>
          </tr>
          <tr>
              <td style="width:11%; height: 32px;">                  
                  <cc2:LabelEntity ID="lblBairro" runat="server" Text="Bairro:" Font-Size="Small" EntitySource="Cliente" style="" ></cc2:LabelEntity>
              </td>
              <td style="width:50%; height: 32px;">                  
                   <cc2:TextBoxEntity ID="txtBairro" runat="server" Width="230px"  Font-Size="Small" EntityProperty="Bairro" EntitySource="Cliente" style="left: 39px; top: 0px;" ></cc2:TextBoxEntity>
              </td>
          </tr>
        <tr>
             <td style="width:13%; height: 26px;">
                 <cc2:LabelEntity ID="lblCidade" runat="server" Text="Cidade:" Font-Size="Small" EntitySource="Cliente" style="" ></cc2:LabelEntity>                         
              </td>
             <td style="width:41%; height: 26px;">
                <cc2:TextBoxEntity ID="txtCidade" runat="server" Width="345px"  Font-Size="Small" EntityProperty="Cidade" EntitySource="Cliente" style="" ></cc2:TextBoxEntity>
              </td>
        </tr> 
        <tr>
             <td style="width:13%; height: 28px;">
                 <cc2:LabelEntity ID="lblUf" runat="server" Text="Uf:" Font-Size="Small" EntitySource="Cliente" style="left: 0px;" ></cc2:LabelEntity>
              </td>
              <td style="width:50%; height: 17px;">
                <cc2:DropDownListCustom ID="ddlUf" runat="server" Width="122px" Font-Size="Small" EntityProperty="Uf" EntitySource="Cliente" style="left: 14px; top: 0px;"  ListNamespaceSource="DataObjectLayer.Business" ListSource="UfCollection"></cc2:DropDownListCustom>
              </td>
        </tr>
        <tr>
              <td style="width:11%; height: 26px;">
                  <cc2:LabelEntity ID="lblCep" runat="server" Text="CEP:" Font-Size="Small" EntitySource="Cliente" ></cc2:LabelEntity>                      
              </td>
             <td style="width:60%; height: 26px;">
                <cc2:TextBoxEntity ID="txtCep" runat="server" Width="94px"  Font-Size="Small" EntityProperty="Cep" EntitySource="Cliente" Mask="99.999-999" ></cc2:TextBoxEntity>
              </td>
        </tr>
        <tr>
              <td style="width:95px; height: 28px;">
                  <cc2:LabelEntity ID="lblTelefone" runat="server" Text="Telefone:" Font-Size="Small" EntitySource="Cliente" ></cc2:LabelEntity>                   
              </td>
             <td style="width:60%; height: 28px;">
                  <cc2:TextBoxEntity ID="txtTelefone" runat="server" Width="168px" Mask="(99)9999-9999"  Font-Size="Small" EntityProperty="Telefone" EntitySource="Cliente" ></cc2:TextBoxEntity>
              </td>
        </tr>
        <tr>
             <td style="width:13%; height: 28px;">
                 <cc2:LabelEntity ID="lblEmail" runat="server" Text="E-mail:" Font-Size="Small"  EntityProperty="Email" EntitySource="Cliente" style="left: 0px;" ></cc2:LabelEntity>
              </td>
             <td style="width:41%; height: 26px;">
                 <cc2:TextBoxEntity ID="txtEmail" runat="server" Width="174px"  Font-Size="Small" EntityProperty="Email" EntitySource="Cliente" style="left: 57px;" ></cc2:TextBoxEntity>
              </td>
        </tr>
          <tr>
             <td style="width:2%">
                    <cc2:LabelEntity ID="lblAtivo" runat="server" Text="Ativo:" Font-Size="Small" EntitySource="Cliente" ></cc2:LabelEntity></td>
             <td style="width:19%">
                    <cc2:DropDownListCustom ID="ddlAtivo" runat="server" width="154px" EntityProperty="Ativo" Visible="true" Font-Size="Small" EntitySource="Cliente" ListNamespaceSource="DataObjectLayer.Business" ListSource="SimNaoCollection"></cc2:DropDownListCustom>
             </td>
          </tr>
          <tr>
             <td style="width:2%">
                    <cc2:LabelEntity ID="lblAnexo" runat="server" Text="Anexo:" Font-Size="Small" EntitySource="Cliente" ></cc2:LabelEntity></td>
             <td style="width:19%">
                    <cc2:FileUploadEntity ID="fupAnexo" runat="server" Font-Size="Small" EntitySource="Cliente"  EntityProperty="Anexo"></cc2:FileUploadEntity>
             </td>
          </tr>
              <tr>
              </tr>
              <tr>
              </tr>
              <tr>
                  <td colspan="2">
                      <cc2:ButtonNewEntity ID="btnNovo" runat="server" Font-Size="Small" Width="74px" Text="Novo" CausesValidation="False"  ButtonSaveEntity="btnSalvar" EntityNamespaceSource="DataObjectLayer.Estoque" EntitySource="Cliente"   />
                      <cc2:ButtonSaveEntity ID="btnSalvar" runat="server" Font-Size="Small" Width="69px" Text="Salvar" Enabled="False" ButtonNewEntity="btnNovo"  GridViewEntity="gridCliente" EntityManagerNamespaceSource="DataObjectLayer.Estoque" EntityManagerSource="EntityManagerCliente" />&nbsp;&nbsp;                    
                  </td>
              </tr>
              <tr>
              </tr>
              <tr>
              </tr>
              <tr>
                <td colspan="2">   
                  <cc2:GridViewEntity ID="gridCliente" runat="server" Height="73px" Width="500px" HasDeleteColumn="True"  EntityManagerNamespaceSource="DataObjectLayer.Estoque" EntityManagerSource="EntityManagerCliente"  EntityNamespaceSource="DataObjectLayer.Estoque" EntitySource="Cliente" ButtonNewEntity="btnNovo" ButtonSaveEntity="btnSalvar" ForeColor="#023454" >
                      <HeaderStyle BackColor="LightBlue" ForeColor="Black" Font-Size="Small" />
                  </cc2:GridViewEntity>
                </td>
              </tr>                    
      </table>
    </div>
    </form>
</body>
</html>
