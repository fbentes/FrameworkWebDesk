using System;
using System.Web.UI;
using System.Text;

namespace DataObjectLayer.WebView.ICN
{
	/// <summary>
	/// Representa uma nova janela do Browser para exibi��o de conte�do HTML.
	/// </summary>
	/// <remarks>
	/// Esta classe utilza recurso do objeto cliente 'window' para representar uma nova janela do browser.
	/// </remarks>
	public class Window
	{
		#region Atributos

		/// <summary>
		/// Largura da janela.
		/// </summary>
		int width;

		/// <summary>
		/// Altura da janela.
		/// </summary>
		int height;

		/// <summary>
		/// URL da p�gina a ser exibida como conte�do da janela.
		/// </summary>
		string url;

		/// <summary>
		/// Ref�ncia do objeto System.Web.UI.Page que ir� chamar a nova janela.
		/// </summary>
		protected Page page;

		/// <summary>
		/// Nome da vari�vel cliente, respons�vel por receber um valor de retorno da Janela.
		/// </summary>		
		string nomeVarJsRetorno;
		
		/// <summary>
		/// Nome da vari�vel cliente, respons�vel por passar a inst�ncia de um objeto cliente para a Janela.
		/// </summary>
		string nomeVarJSParam;

		/// <summary>
		/// Define se a janela poder� ser redimensionada pelo usu�rio.
		/// </summary>
		bool resizable;

		/// <summary>
		/// Define se a janela possuir� ScrollBars.
		/// </summary>
		bool scrollbars;
		#endregion
		
		#region Propriedades		
			
		/// <summary>
		/// Inst�ncia da classe Page, referente a p�gina que chamar� a janela.
		/// </summary>
		public Page Page
		{
			get
			{
				return page;
			}
		}
		
		/// <summary>
		/// Largura da Janela em pixel.
		/// </summary>
		public int Width
		{
			get
			{
				return width;	
			}
			set	
			{
				width = value;				
			}	
		}
		
		/// <summary>
		/// Altura da janela em pixel.
		/// </summary>
		public int Height
		{
			get
			{
				return height;	
			}
			set	
			{
				height = value;				
			}	
		}
		
		/// <summary>
		/// URL da p�gina a ser exibida como conte�do da janela.
		/// </summary>
		public string URL
		{
			get 
			{
				return url;
			}
			set
			{
				url = value;
			}
		}
		
		/// <summary>
		/// Nome da vari�vel cliente, respons�vel por receber um valor de retorno da Janela.
		/// </summary>		
		public string  NomeVarJsRetorno
		{
			get 
			{	
				return nomeVarJsRetorno;
			}
			set
			{
				nomeVarJsRetorno = value;
			}
		}
		
		/// <summary>
		/// Nome da vari�vel cliente, respons�vel por passar a inst�ncia de um objeto cliente para a Janela.
		/// </summary>
		public string  NomeVarJSParam
		{
			get 
			{
				return nomeVarJSParam;
			}
			set
			{
				nomeVarJSParam = value;
			}
		}
		
		/// <summary>
		/// Define se a janela poder� ser redimensionada pelo usu�rio.
		/// </summary>
		public bool Resizable
		{
			get
			{
				return resizable;
			}
			set
			{
				resizable = value;
			}
		}		
		
		/// <summary>
		/// Define se a janela possuir� ScrollBars.
		/// </summary>
		public bool Scrollbars
		{
			get
			{
				return scrollbars;
			}
			set
			{	
				scrollbars = value;
			}
		}
		
		#endregion 		

		#region M�todos P�blicos

		/// <summary>
		/// Cria uma nova inst�ncia de Window. Por default os seguintes valores ser�o assumidos : 
		/// resizable = false ,scrollbars = false, NomeVarJsRetorno = "Ret, NomeVarJSParam = "null". 
		/// </summary>
		public Window()
		{
			resizable = false;
			scrollbars = false;
			NomeVarJsRetorno = "Ret";
			NomeVarJSParam = "null";
		}
		
		/// <summary>
		/// Cria uma nova inst�ncia de Window passando a inst�ncia da classe Page, referente a p�gina que chamar� a janela. 
		/// Por default os seguintes valores ser�o assumidos: 
		/// resizable = false, scrollbars = false, NomeVarJsRetorno = "Ret, NomeVarJSParam = "null". 
		/// </summary>
		public Window(Page pPage)
		{
			page = pPage;	
			new Window();
		}

		/// <summary>
		/// Conte�do referente ao script (JavaScript) gerado para exibi��o da janela.
		/// </summary>
		/// <returns>Script para exibi��o da Janela.</returns>
		/// <remarks>
		/// Este m�todo geralmente  � usado quando se t�m a inten��o utilizar o script retornado,
		/// para trabalhar principalmente o valor recebido na vari�vel de retorno definida por <c>NomeVarJsRetorno</c>. 
		/// Ou seja o script retornado ir� compor um script maior.
    	/// Obs: As tags '<script></script>' n�o fazem parte do valor de retorno.  
		///</remarks>
		public virtual string GetJSReference()
		{	

			StringBuilder strParam = new StringBuilder();
			strParam.Append("width=" + Width.ToString() + ",");
			strParam.Append("height=" + Height.ToString() + ",");
			strParam.Append("status=yes,toolbar=no,menubar=no,location=no");
			strParam.Append(",resizable =" + (Resizable?"yes":"no"));
			strParam.Append(",scrollbars=" + (Scrollbars?"yes":"no"));
			
			StringBuilder strWindow = new StringBuilder();
			strWindow.Append(NomeVarJsRetorno + " =window.open('" + URL + "'," + nomeVarJSParam + ",'" + strParam + "');");
			
			return strWindow.ToString();
		}

		/// <summary>
		/// Exibe nova janela.
		/// </summary>
		public virtual void Show()
		{
			Random rnd = new Random();
			rnd.Next();
			string strNomeFunction = "fun_" + rnd.Next().ToString();
	
			StringBuilder strScript = new StringBuilder();
			strScript.Append("<script>");
			strScript.Append("var " + NomeVarJsRetorno + ";");
			strScript.Append("function "  + strNomeFunction  + "(){");
			strScript.Append(GetJSReference() + ";");
			strScript.Append("window.clearInterval(TimeId);");
			strScript.Append("}");
			strScript.Append("TimeId = window.setInterval('" + strNomeFunction + "()',50);");
			strScript.Append("</script>");

			Page.ClientScript.RegisterStartupScript(this.GetType(), "WindowDialog",strScript.ToString());			
		}
		#endregion	
	}
}
