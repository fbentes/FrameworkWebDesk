using System;
using System.Text;
using System.Web.UI;

namespace DataObjectLayer.WebView.ICN
{
	/// <summary>
	/// Representa os tipos de exibição de Janela Dialog Box disponíveis.
	/// </summary>
	public enum eExibicaoWindowDialog{Modal,Modless};
	
	/// <summary>
	/// Representa uma nova janela do tipo Dialog Box para exibição de conteúdo HTML.
	/// </summary>
	/// <remarks>
	/// Esta classe utilza recurso do objeto cliente 'window', mais especificamente o método 'showModelessDialog e showModalDialog'
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

		#region Métodos Públicos
		/// <summary>
		/// Cria uma nova instância de Window passando a instância da classe Page, referente a página que chamará a janela. 
		/// Por default os seguintes valores serão assumidos: 
		/// NomeVarJsRetorno = "Ret, NomeVarJSParam = "null". 
		/// </summary>
		public WindowDialog(Page pPage)
		{
			page = pPage;
			NomeVarJsRetorno = "Ret";
			NomeVarJSParam = "null";
		}

		/// <summary>
		/// Conteúdo referente ao script (JavaScript) gerado para exibição da janela do tipo Dialog Box.
		/// </summary>
		/// <returns>Script para exibição da Janela do tipo Dialog Box.</returns>
		/// <remarks>
		/// Este método geralmente  é usado quando se têm a intenção utilizar o script retornado,
		/// para trabalhar principalmente o valor recebido na variável de retorno definida por <c>NomeVarJsRetorno</c>. 
		/// Ou seja o script retornado irá compor um script maior.
		/// Obs: As tags '<script></script>' não fazem parte do valor de retorno.  
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
