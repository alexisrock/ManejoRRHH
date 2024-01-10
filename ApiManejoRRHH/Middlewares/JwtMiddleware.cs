﻿using Core.Interfaces;
using Core.Repository;

namespace ApiManejoRRHH.Middlewares
{
    /// <summary>
    /// Middleware de comprobacion del token
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate requestDelegate;


        /// <summary>
        /// Constructor
        /// </summary>
        public JwtMiddleware(RequestDelegate requestDelegat )
        {
            this.requestDelegate = requestDelegat;
           
        }

        /// <summary>
        /// Metodo para validar el token
        /// </summary>

        public async Task InvokeAsync(HttpContext context, ITokenService tokenService) 
        {           
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if ((token is not null) && ValidateToken(token, tokenService))
            {
                context.Items["UserId"] = "ALEXIS";
            }
            await requestDelegate(context);
        }

        private bool ValidateToken(string token, ITokenService tokenService)
        {
           
           
            return tokenService.ValidateToken(token).Result;
        }


    }
}
