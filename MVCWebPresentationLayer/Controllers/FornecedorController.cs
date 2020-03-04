using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class FornecedorController : Controller
    {
        FornecedorService fsvc = new FornecedorService();
        CategoriaService csvc = new CategoriaService();

        public async Task<ActionResult> Cadastrar()
        {
            List<FornecedorDTO> fornecedores = await fsvc.GetData();
            List<CategoriaDTO> categorias = await csvc.GetData();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FornecedorDTO, FornecedorQueryViewModel>();
                cfg.CreateMap<CategoriaDTO, CategoriaQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();

            FornecedorQueryViewModel dadosFornecedores = mapper.Map<FornecedorQueryViewModel>(fornecedores);
            CategoriaQueryViewModel dadosCategorias = mapper.Map<CategoriaQueryViewModel>(categorias);

            ViewBag.Fornecedores = dadosFornecedores;
            ViewBag.Categorias = dadosCategorias;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(FornecedorViewModel viewModel)
        {
            FornecedorService svc = new FornecedorService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FornecedorViewModel, FornecedorDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteViewModel em um ClienteDTO.
            FornecedorDTO dto = mapper.Map<FornecedorDTO>(viewModel);

            try
            {
                await svc.Insert(dto);

                //Se funcionou, para a página inicial.
                return RedirectToAction("Index", "Fornecedor");
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
                FornecedorService svc = new FornecedorService();
                List<FornecedorDTO> fornecedores = await svc.GetData();

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FornecedorDTO, FornecedorQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                List<FornecedorQueryViewModel> dados = mapper.Map<List<FornecedorQueryViewModel>>(fornecedores);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}