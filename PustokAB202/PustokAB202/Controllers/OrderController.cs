using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;
using System.Security.Claims;

namespace PustokAB202.Controllers
{
	public class OrderController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public OrderController(AppDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}


		public async Task<IActionResult> Checkout()
		{
			OrderVM orderVM = new OrderVM();

			if (User.Identity.IsAuthenticated)
			{
				AppUser user = await _userManager.Users
					.Include(x => x.BasketItems)
					.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

				orderVM.Name = user.Name;
				orderVM.Surname = user.Surname;
				orderVM.Email = user.Email;

				foreach (var item in user.BasketItems)
				{
					Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == item.BookId);
					if (book is not null)
					{
						orderVM.CheckoutItemVMs.Add(new CheckoutItemVM
						{
							Price = book.SalePrice - book.Discount,
							Count = item.Count,
							BookName = book.Name
						});

					}

				}
			}
			else
			{
				string json = Request.Cookies["Books"];

				if (json != null)
				{
					List<BookCookieVM> cookies = JsonConvert.DeserializeObject<List<BookCookieVM>>(json);

					foreach (var item in cookies)
					{
						Book book = await _context.Books
							.Where(x => x.IsDeleted == false)
							.Include(x => x.BookImages.Where(x => x.IsPrimary == true))
							.FirstOrDefaultAsync(x => x.Id == item.Id);

						if (book is not null)
						{
							orderVM.CheckoutItemVMs.Add(new CheckoutItemVM
							{
								BookName = book.Name,
								Price = book.SalePrice - book.Discount,
								Count = item.Count,
							});
						}
					}

				}
			}


			return View(orderVM);
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(OrderVM orderVM)
		{


			if (User.Identity.IsAuthenticated)
			{
				AppUser user = await _userManager.Users
					.Include(x => x.BasketItems)
					.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

				if (!ModelState.IsValid)
				{
					orderVM.Name = user.Name;
					orderVM.Surname = user.Surname;
					orderVM.Email = user.Email;

					foreach (var item in user.BasketItems)
					{
						Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == item.BookId);
						if (book is not null)
						{
							orderVM.CheckoutItemVMs.Add(new CheckoutItemVM
							{
								Price = book.SalePrice - book.Discount,
								Count = item.Count,
								BookName = book.Name
							});

						}

					}
					return View(orderVM);
				}

				Order order = new Order
				{
					AppUserId = user.Id,
					Status = Utilities.Enums.OrderStatus.Pending,
					Adress = orderVM.Adress,
					Name = user.Name,
					Surname = user.Surname,
					Email = user.Email,
					CreatedAt = DateTime.Now,
					TotalAmount = 0,
					OrderItems = new List<OrderItem>()
				};

				decimal total = 0;

				foreach (var item in user.BasketItems)
				{
					Book book = await _context.Books
						.Include(x => x.BookImages.Where(y => y.IsPrimary == true))
						.FirstOrDefaultAsync(x => x.Id == item.BookId);

					total += item.Count * (book.SalePrice - book.Discount);


					if (book is not null)
					{
						order.OrderItems.Add(new OrderItem
						{
							Count = item.Count,
							SalePrice = book.SalePrice,
							CostPrice = book.CostPrice,
							Discount = book.Discount,
							Image = book.BookImages[0].Image,
							BookId = book.Id,
							BookName = book.Name,
							Subtotal = item.Count * (book.SalePrice - book.Discount)
						});
					}
				}

				order.TotalAmount = total;
				await _context.Orders.AddAsync(order);
				user.BasketItems = new List<BasketItem>();
				await _context.SaveChangesAsync();


			}
			else
			{
				string json = Request.Cookies["Books"];
				List<BookCookieVM> cookies = new List<BookCookieVM>();
				if (json is not null)
				{
					cookies = JsonConvert.DeserializeObject<List<BookCookieVM>>(json);
				}

				if (!ModelState.IsValid)
				{
					if (json != null)
					{

						foreach (var item in cookies)
						{
							Book book = await _context.Books
								.Where(x => x.IsDeleted == false)
								.Include(x => x.BookImages.Where(x => x.IsPrimary == true))
								.FirstOrDefaultAsync(x => x.Id == item.Id);

							if (book is not null)
							{
								orderVM.CheckoutItemVMs.Add(new CheckoutItemVM
								{
									BookName = book.Name,
									Price = book.SalePrice - book.Discount,
									Count = item.Count,
								});
							}
						}

					}
					else
					{
						orderVM.CheckoutItemVMs = new List<CheckoutItemVM>();
					}
					return View(orderVM);
				}



				Order order = new Order
				{
					Status = Utilities.Enums.OrderStatus.Pending,
					Adress = orderVM.Adress,
					Name = orderVM.Name,
					Surname = orderVM.Surname,
					Email = orderVM.Email,
					CreatedAt = DateTime.Now,
					TotalAmount = 0,
					OrderItems = new List<OrderItem>()
				};

				decimal total = 0;

				if (json != null)
				{
					foreach (var item in cookies)
					{
						Book book = await _context.Books
							.Include(x => x.BookImages.Where(y => y.IsPrimary == true))
							.FirstOrDefaultAsync(x => x.Id == item.Id);

						total += item.Count * (book.SalePrice - book.Discount);


						if (book is not null)
						{
							order.OrderItems.Add(new OrderItem
							{
								Count = item.Count,
								SalePrice = book.SalePrice,
								CostPrice = book.CostPrice,
								Discount = book.Discount,
								Image = book.BookImages[0].Image,
								BookId = book.Id,
								BookName = book.Name,
								Subtotal = item.Count * (book.SalePrice - book.Discount)
							});
						}
					}

					order.TotalAmount = total;
					await _context.Orders.AddAsync(order);
					await _context.SaveChangesAsync();

					Response.Cookies.Delete("Books");

				}

			}

			return RedirectToAction("Index", "Home");
		}
	}
}
