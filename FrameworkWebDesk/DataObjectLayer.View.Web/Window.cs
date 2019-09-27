using System;
using System.Web.UI;
using System.Text;

namespace DataObjectLayer.WebView.ICN
{
	/// <summary>
	/// Representa uma nova janela do Browser para exibição de conteúdo HTML.
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
		/// URL da página a ser exibida como conteúdo da janela.
		/// </summary>
		string url;

		/// <summary>
		/// Refência do objeto System.Web.UI.Page que irá chamar a nova janela.
		/// </summary>
		protected Page page;

		/// <summary>
		/// Nome da variável cliente, responsável por receber um valor de retorno da Janela.
		/// </summary>		
		string nomeVarJsRetorno;
		
		/// <summary>
		/// Nome da variável cliente, responsável por passar a instância de um objeto cliente para a Janela.
		/// </summary>
		string nomeVarJSParam;

		/// <summary>
		/// Define se a janela poderá ser redimensionada pelo usuário.
		/// </summary>
		bool resizable;

		/// <summary>
		/// Define se a janela possuirá ScrollBars.
		/// </summary>
		bool scrollbars;
		#endregion
		
		#region Propriedades		
			
		/// <summary>
		/// Instância da classe Page, referente a página que chamará a janela.
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
		/// URL da página a ser exibida como conteúdo da janela.
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
		/// Nome da variável cliente, responsável por receber um valor de retorno da Janela.
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
		/// Nome da variável cliente, responsável por passar a instância de um objeto cliente para a Janela.
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
		/// Define se a janela poderá ser redimensionada pelo usuário.
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
		/// Define se a janela possuirá ScrollBars.
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

		#region Métodos Públicos

		/// <summary>
		/// Cria uma nova instância de Window. Por default os seguintes valores serão assumidos : 
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
		/// Cria uma nova instância de Window passando a instância da classe Page, referente a página que chamará a janela. 
		/// Por default os seguintes valores serão assumidos: 
		/// resizable = false, scrollbars = false, NomeVarJsRetorno = "Ret, NomeVarJSParam = "null". 
		/// </summary>
		public Window(Page pPage)
		{
			page = pPage;	
			new Window();
		}

		/// <summary>
		/// Conteúdo referente ao script (JavaScript) gerado para exibição da janela.
		/// </summary>
		/// <returns>Script para exibição da Janela.</returns>
		/// <remarks>
		/// Este método geralmente  é usado quando se têm a intenção utilizar o script retornado,
		/// para trabalhar principalmente o valor recebido na variável de retorno definida por <c>NomeVarJsRetorno</c>. 
		/// Ou seja o script retornado irá compor um script maior.
    	/// Obs: As tags '<script></script>' não fazem parte do valor de retorno.  
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
