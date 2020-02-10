using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using AspnetCoreStudy.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet] //default 값으로 설정됨
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) //null허용x인 필수값들 모두 받았는지? -> 사용자 이름도 required기때문에 오류남, 그래서 뷰모델 따로만들어줌
            {
                using (var db = new AspnetNoteDBContext()) //오픈, 커넥션 후 자동으로 닫기위해 using 사용
                    // sql에 접근하는 db 라는 변수 생성.
                {
                    // Linq 쿼리식
                    // => 는 go to 
                    //var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId && u.UserPassword == model.UserPassword);
                    var user = db.Users
                        .FirstOrDefault(u => u.UserId.Equals(model.UserId) &&
                                             u.UserPassword.Equals(model.UserPassword));
                                                            //equals를 쓰는 이유는, 변수생성해서 메모리 할당 안해도 되기때문.

                    if (user != null)
                    {
                        //로그인 성공 시
                        //HttpContext.Session.SetInt32(key, value);
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home");     // 로그인 성공 페이지로 이동, return이기 떄문에 뒤의 구문 안탐.
                    }
                    //로그인 실패 시
                    ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
                }
                return RedirectToAction("index", "Home"); //Home컨트롤러의 index액션으로 넘김 (view)
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("index", "Home");
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]  //post방식으로 전송했을 때의 메서드 오버로딩
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid) //null허용x인 필수값들 모두 받았는지?
            {
                using (var db = new AspnetNoteDBContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("index", "Home"); //Home컨트롤러의 index액션으로 넘김 (view)
            }
            return View();
        }
    }
}