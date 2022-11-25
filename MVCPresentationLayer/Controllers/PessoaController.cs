using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models.Pessoa;
using Service;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _appEnvironment;

        public PessoaController(IPessoaService pessoaService, IMapper mapper, IHttpContextAccessor context, IWebHostEnvironment hosting)
        {
            this._appEnvironment = hosting;
            this._pessoaService = pessoaService;
            this._mapper = mapper;
            this._accessor = context;
        }

        public async Task<IActionResult> Index()
        {
            DataResponse<Pessoa> response = await this._pessoaService.GetAll();//GetAll
            if (!response.Sucesso)
            {
                ViewBag.Errors = response.Mensagem;
            }
            List<PessoaQueryViewModel> pessoas =
                _mapper.Map<List<PessoaQueryViewModel>>(response.Data);

            return View(pessoas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(PessoaInsertViewModel viewModel)
        {
            Pessoa pessoa = _mapper.Map<Pessoa>(viewModel);

            Response response = await _pessoaService.Insert(pessoa);
            if (!response.Sucesso)
            {
                ViewBag.Error = response.Mensagem;
                return View();
            }

            return await this.Login(new PessoaLogin()
            {
                Email = viewModel.Email,
                Senha = viewModel.Senha
            });

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PessoaLogin pessoaLogin)
        {
            SingleResponse<Pessoa> resposta = await _pessoaService.Authenticate(pessoaLogin.Email, pessoaLogin.Senha);
            if (!resposta.Sucesso)
            {
                ViewBag.Errors = resposta.Mensagem;
                return View();
            }
            //Se chegou aqui, devemos criar o cookies pq a autenticação funcionou!
            List<Claim> userClaims = new()
                {
                    //define o cookie
                    new Claim(ClaimTypes.Name, resposta.Item.Email),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, resposta.Item.ID.ToString())
            };
            var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
            var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });
            //cria o cookie

            if (this.HttpContext == null)
            {
                await this._accessor.HttpContext.SignInAsync(userPrincipal);
            }
            else
            {
                await HttpContext.SignInAsync(userPrincipal);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            string id = User.Claims.ToList()[2].Value;


            SingleResponse<Pessoa> pessoaResponse = await this._pessoaService.GetByID(int.Parse(id));
            if (!pessoaResponse.Sucesso)
            {
                return RedirectToAction("Index", "Home");
            }

            PessoaProfileViewModel pessoaProfile =
                 _mapper.Map<PessoaProfileViewModel>(pessoaResponse.Item);

            return View(pessoaProfile);
        }
        public IActionResult Profile(PessoaProfileViewModel pessoa)
        {
            if (pessoa is null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Editar()
        {
            string id = User.Claims.ToList()[2].Value;

            SingleResponse<Pessoa> response = await _pessoaService.GetByID(int.Parse(id));
            if (!response.Sucesso)
            {
                return RedirectToAction("Home");
            }
            PessoaUpdateViewModel pessoa =
                _mapper.Map<PessoaUpdateViewModel>(response.Item);
            return View(pessoa);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Editar(PessoaUpdateViewModel viewModel)
        {
            SingleResponse<Pessoa> response = await _pessoaService.Authenticate(viewModel.Email, viewModel.Senha);
            if (!response.Sucesso)
            {
                ViewBag.Error = "Senha inválida.";
                return await this.Editar();
            }

            if (!string.IsNullOrWhiteSpace(viewModel.NovaSenha))
            {
                viewModel.Senha = viewModel.NovaSenha;
            }
            Pessoa p = _mapper.Map<Pessoa>(viewModel);

            Response r = await _pessoaService.Update(p);
            if (r.Sucesso)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Error = r.Mensagem;
            return await this.Editar();
        }
    }
}

