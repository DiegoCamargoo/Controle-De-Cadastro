﻿using ControleDeContatos.Enums;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters;

public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
        if (string.IsNullOrEmpty(sessaoUsuario))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });

        }
        else
        {
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            if (usuario is null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });

            }
            if (usuario.Perfil != PerfilEnum.Admin)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });

        }

        base.OnActionExecuted(context);
    }
}
