using GoFinance.Domain.Entities;
using System.Text;

namespace GoFinances.Infra.Helpers
{
    public static class TemplatesEmail
    {
        public static string ObterHtmlResetarSenhaUsuario(Usuario usuario, string urlSite)
        {
            var url = $"{urlSite}?token={usuario.TokenAlteracaoSenha}";
            var str = new StringBuilder();

            str.AppendLine("<!DOCTYPE html>");
            str.AppendLine("<html lang='pt-BR'>");
            str.AppendLine("<head>");
            str.AppendLine("    <meta charset='UTF-8'>");
            str.AppendLine("    <meta http-equiv='X-UA-Compatible' content='IE=edge'>");
            str.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            str.AppendLine("    <title>Recuperar Senha</title>");
            str.AppendLine("</head>");
            str.AppendLine("<body>");
            str.AppendLine("    <table style='width: 600px;'>");
            str.AppendLine("        <tr>");
            str.AppendLine("            <td style='background: #7542ea; height: 90px;'>");
            str.AppendLine("                <div style='width: 500px; margin: auto;'>");
            str.AppendLine("                    <h3");
            str.AppendLine("                        style='color: #fff; font-size: 23px; font-family: sans-serif; font-weight: 900; text-align: center;'>");
            str.AppendLine("                        Recuperar Senha");
            str.AppendLine("                    </h3>");
            str.AppendLine("                </div>");
            str.AppendLine("            </td>");
            str.AppendLine("        </tr>");
            str.AppendLine("        <tr>");
            str.AppendLine("            <td style='padding-bottom: 64px; padding-left: 26px; padding-top: 12px;'>");
            str.AppendLine("                <h4 style='font-size: 20px; font-family: sans-serif; margin-bottom: 8px; color: #525252;'>");
            str.AppendLine($"                    Olá, {usuario.Nome}");
            str.AppendLine("                </h4>");
            str.AppendLine("                <p style='font-family: sans-serif; font-size: 14px; color: #808080;'>");
            str.AppendLine("                    Para recuperar sua senha acesse o link abaixo:");
            str.AppendLine("                </p>");
            str.AppendLine($"                <a href='{url}' style='text-decoration: none;' target='_blank'>Recuperar senha</a>");
            str.AppendLine("            </td>");
            str.AppendLine("        </tr>");
            str.AppendLine("        <tr>");
            str.AppendLine("            <td style='background: #8257e5; height: 60px;'>");
            str.AppendLine("                <div style='width: 500px; margin: auto;'>");
            str.AppendLine("                    <p");
            str.AppendLine("                        style='color: #fff; font-family: sans-serif; text-align: end; font-weight: 600; font-size: 14px;'>");
            str.AppendLine("                        Go Finances");
            str.AppendLine("                    </p>");
            str.AppendLine("                </div>");
            str.AppendLine("            </td>");
            str.AppendLine("        </tr>");
            str.AppendLine("    </table>");
            str.AppendLine("</body>");
            str.AppendLine("</html>");

            return str.ToString();
        }

    }
}
