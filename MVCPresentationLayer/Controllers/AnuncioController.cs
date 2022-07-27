using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models.Anuncio;
using Service;
using Service.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        private IPessoaService _pessoaService;
        private IAnuncioService _anuncioService;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public AnuncioController(IAnuncioService anuncioService, IMapper mapper, IWebHostEnvironment environment, IPessoaService pessoaService)
        {
            this._pessoaService = pessoaService;
            this._anuncioService = anuncioService;  
            this._mapper = mapper;
            this._appEnvironment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Detalhes(AnuncioInteresseViewModel anuncio)
        {
            int? anuncioID = anuncio.AnuncioID;
            if (!anuncioID.HasValue || anuncioID.Value == 0)
            {
                return RedirectToAction("Index", "Anuncio");
            }
            string id = User.Claims.ToList()[2].Value;
            Response response = await this._anuncioService.VincularInteresse(int.Parse(id), anuncioID.Value);
            if (response.Sucesso)
            {
                return RedirectToAction("Index", "Anuncio");
            }

            ViewBag.Errors = response.Mensagem;
            return await Detalhes(anuncio.AnuncioID);

        }



        public async Task<IActionResult> Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Anuncio");
            }

            SingleResponse<Anuncio> anuncioResponse = await this._anuncioService.GetByID(id.Value);
            if (!anuncioResponse.Sucesso)
            {
                return RedirectToAction("Index", "Anuncio");
            }
            //AnuncioInsertViewModel anuncioInsert = _mapper.Map<AnuncioInsertViewModel>(anuncioResponse.Item);
            //if (anuncioInsert.SegundaFoto == null && || anuncioInsert.TerceiraFoto == null)
            //{
            //    return View(anuncioInsert);
            //}
            DetalhesQueryViewModel anuncio =
                 _mapper.Map<DetalhesQueryViewModel>(anuncioResponse.Item);

            return View(anuncio);
        }


        [HttpPost]
        public async Task<IActionResult> PerfilAnuncio(PessoaInteresseViewModel pessoaInteresse)
        {
            int? pessoaID = pessoaInteresse.PessoaID;
            if (!pessoaID.HasValue || pessoaID.Value == 0)
            {
                return RedirectToAction("Index", "Anuncio");
            }
            string id = User.Claims.ToList()[2].Value;
            Response response = await this._anuncioService.GetByID(int.Parse(id));
            if (response.Sucesso)
            {
                return RedirectToAction("Index", "Anuncio");
            }

            ViewBag.Errors = response.Mensagem;
            return await PerfilAnuncio(pessoaInteresse.PessoaID);
        }
        public async Task<IActionResult> PerfilAnuncio(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Anuncio");
            }

            SingleResponse<Pessoa> pessoaResponse = await this._pessoaService.GetByID(id.Value);
            if (!pessoaResponse.Sucesso)
            {
                return RedirectToAction("Index", "Anuncio");
            }

            PessoaPerfilQueryViewModel PerfilAnuncio =
                 _mapper.Map<PessoaPerfilQueryViewModel>(pessoaResponse.Item);

            return View(PerfilAnuncio);
        }
        
        public async Task<IActionResult> Index()
        {
            DataResponse<Anuncio> response = await this._anuncioService.GetAll();//GetAll
            if (!response.Sucesso)
            {
                ViewBag.Errors = response.Mensagem;
            }
            List<DetalhesQueryViewModel> anuncios =
                _mapper.Map<List<DetalhesQueryViewModel>>(response.Data);

            return View(anuncios);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AnuncioInsertViewModel viewModel)
            {
            viewModel.EhCastrado = Request.Form["EhCastrado"].ToString() == "on" ? true : false;
            viewModel.EhDeficiente = Request.Form["EhDeficiente"].ToString() == "on" ? true : false;

            if (viewModel.PrimeiraFoto == null || viewModel.SegundaFoto == null || viewModel.TerceiraFoto == null || viewModel.PrimeiraFoto.Length == 0
                || viewModel.SegundaFoto.Length == 0 || viewModel.TerceiraFoto.Length == 0
                || !FileHelper.IsValidFile(viewModel.PrimeiraFoto.FileName) || !FileHelper.IsValidFile(viewModel.SegundaFoto.FileName) ||
                !FileHelper.IsValidFile(viewModel.TerceiraFoto.FileName))
            {
                ViewBag.Error = "Três imagens devem ser enviadas."; 
                return View();
            }
            /// else
            /// {
            ///     MemoryStream ms = new MemoryStream();
            ///     Bitmap bm = new Bitmap(ms);
            ///     if (bm.Height <= 800 || bm.Width <= 600)
            ///     {
            ///         ViewBag.Error = "A imagem deve conter entre 800 x 600 pixels";
            ///         return RedirectToAction("Anuncio", "Detalhes");
            ///     }
            ///
            /// }

            if (viewModel.PrimeiraFoto == null || viewModel.PrimeiraFoto.Length == 0 || !FileHelper.IsValidFile(viewModel.PrimeiraFoto.FileName))
            {
                ViewBag.Error = "Ao menos uma imagem deve ser enviada para concluir o cadastro. Extensões válidas: .jpg | .jpeg | .gif | .png | .bmp";
                return RedirectToAction();
            }
            Anuncio anuncio = _mapper.Map<Anuncio>(viewModel);
            string id = User.Claims.ToList()[2].Value;
            anuncio.PessoaID = int.Parse(id);
            Response response = await _anuncioService.Insert(anuncio);
            if (!response.Sucesso)
            {
                ViewBag.Error = response.Mensagem;
                return View();
            }

            string pastaASerCriada = _appEnvironment.WebRootPath + "\\imgSistema\\" + anuncio.ID.ToString();
            Directory.CreateDirectory(pastaASerCriada);

            using (FileStream stream = new FileStream(pastaASerCriada + "\\1.jpg", FileMode.Create))
            {
                await viewModel.PrimeiraFoto.CopyToAsync(stream);
            }

            if (viewModel.SegundaFoto != null)
            {
                using (FileStream stream = new FileStream(pastaASerCriada + "\\2.jpg", FileMode.Create))
                {
                    await viewModel.SegundaFoto.CopyToAsync(stream);
                }
            }

            if (viewModel.TerceiraFoto != null)
            {
                using (FileStream stream = new FileStream(pastaASerCriada + "\\3.jpg", FileMode.Create))
                {
                    await viewModel.TerceiraFoto.CopyToAsync(stream);
                }
            }

            //TODO - Necessidade de validar via bitmap a resolução da foto


            return RedirectToAction("Index");

        }




        [HttpPost]
        public async Task<IActionResult> Deletar(int AnuncioID)
        {
            _ = await this._anuncioService.Excluir(AnuncioID);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Doar(int AnuncioID)
        {
            Response r = await this._anuncioService.DoarAnimal(AnuncioID);
            if (!r.Sucesso)
            {
                ViewBag.Errors = r.Mensagem;
                return RedirectToAction("Detalhes");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
