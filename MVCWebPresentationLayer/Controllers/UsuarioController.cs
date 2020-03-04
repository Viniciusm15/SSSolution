using AutoMapper;
using BLL.Impl;
using DTO;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Impl;
using Common;

namespace MVCWebPresentationLayer.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioService svc = new UsuarioService();

        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioViewModel viewModel)
        {
            UsuarioService svc = new UsuarioService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuarioViewModel, UsuarioDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteViewModel em um ClienteDTO.
            UsuarioDTO dto = mapper.Map<UsuarioDTO>(viewModel);

            try
            {
                await svc.Insert(dto);

                //Se funcionou, para a página inicial.
                return RedirectToAction("Index", "Usuario");
            }
            catch (NecoException ex)
            {
                //Se caiu aqui, o ClienteService lançou a exceção por validação de campos.
                ViewBag.ValidationErrors = ex.Erros;
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o ClienteService lançou a exceção genérica, provavelmente por falha de acesso ao banco.
                ViewBag.ErrorMessage = ex.Message;
            }

            //Se chegou aqui temos erros.
            return View();
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                UsuarioService svc = new UsuarioService();
                List<UsuarioDTO> usuarios = await svc.GetData();

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UsuarioDTO, UsuarioQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteViewModel. (Lista de clientes)
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<UsuarioQueryViewModel> dados = mapper.Map<List<UsuarioQueryViewModel>>(usuarios);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email, string senha)
        {
            try
            {
                //Se chegou aqui, sucesso.
                UsuarioDTO usuario = await svc.Autententicar(email, senha);

                //Criando um arquivo na máquina do usuário.
                HttpCookie cookie = new HttpCookie("USERIDENTITY", usuario.ID.ToString());
                cookie.Expires = DateTime.MaxValue;

                Response.Cookies.Add(cookie);
                return RedirectToAction("Index","Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }

            return View();
        }
    }
}