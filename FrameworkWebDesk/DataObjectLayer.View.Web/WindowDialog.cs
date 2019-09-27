using System;
using System.Text;
using System.Web.UI;

namespace DataObjectLayer.WebView.ICN
{
	/// <summary>
	/// Representa os tipos de exibi��o de Janela Dialog Box dispon�veis.
	/// </summary>
	public enum eExibicaoWindowDialog{Modal,Modless};
	
	/// <summary>
	/// Representa uma nova janela do tipo Dialog Box para exibi��o de conte�do HTML.
	/// </summary>
	/// <remarks>
	/// Esta classe utilza recurso do objeto cliente 'window', mais especificamente o m�todo 'showModelessDialog e showModalDialog'
	/// para representar uma nova janela do tipo Dialog Box .
	/// </remarks>
	public class WindowDialog : Window
	{	
		#region Atributos
		eExibicaoWindowDialog exibicao;
		#endregion
		
		#region Propriedades
		/// <summary>
		/// Tipo de Janela Dialog Box (Modal e ModLess)
		/// </summary>
		public eExibicaoWindowDialog Exibicao
		{
			get
			{
				return exibicao;	
			}
			set	
			{
				exibicao = value;				
			}			
		}
		#endregion

		#region M�todos P�blicos
		/// <summary>
		/// Cria uma nova inst�ncia de Window passando a inst�ncia da classe Page, referente a p�gina que chamar� a janela. 
		/// Por default os seguintes valores ser�o assumidos: 
		/// NomeVarJsRetorno = "Ret, NomeVarJSParam = "null". 
		/// </summary>
		public WindowDialog(Page pPage)
		{
			page = pPage;
			NomeVarJsRetorno = "Ret";
			NomeVarJSParam = "null";
		}

		/// <summary>
		/// Conte�do referente ao script (JavaScript) gerado para exibi��o da janela do tipo Dialog Box.
		/// </summary>
		/// <returns>Script para exibi��o da Janela do tipo Dialog Box.</returns>
		/// <remarks>
		/// Este m�todo geralmente  � usado quando se t�m a inten��o utilizar o script retornado,
		/// para trabalhar principalmente o valor recebido na vari�vel de retorno definida por <c>NomeVarJsRetorno</c>. 
		/// Ou seja o script retornado ir� compor um script maior.
		/// Obs: As tags '<script></script>' n�o fazem parte do valor de retorno.  
		///</remarks>
		public override string GetJSReference()
		{	
			
			StringBuilder strParam = new StringBuilder();
			strParam.Append("dialogWidth:" + Width.ToString() + "px;");
			strParam.Append("dialogHeight:" + Height.ToString() + "px;");
			strParam.Append("help:0;status:0;unadorned:1;edge:0;center:yes;");
			strParam.Append("resizable:" + (Resizable?"yes":"no"));
			strParam.Append(";scroll:" + (Scrollbars?"yes":"no"));

			StringBuilder strWindow = new StringBuilder();
			strWindow.Append(NomeVarJsRetorno + " = window.showModalDialog('" + URL + "'," + NomeVarJSParam + ",'" + strParam + "');");
			
			return strWindow.ToString();
		}
		#endregion
	}
}
