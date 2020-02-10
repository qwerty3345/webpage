using System.Collections.Generic;
using System.Linq;
using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AspnetCoreStudy.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDBContext())
            {
                var list = db.Notes.ToList();
                return View(list); //view 안에 무언갈 담게되면 그 담은 것을 model이라고 인식함. VIEW 창에 가서는 model쓸것!
            }
        }

        /// <summary>
        /// 게시글 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                using(var db = new AspnetNoteDBContext())
                {
                    db.Notes.Add(model);
                    if(db.SaveChanges() > 0) // commit을 해 주는 것. _ 무슨개념이지..?
                    {
                        return Redirect("Index"); // = return RedirectToAction("Index", "Note");
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다."); //modelstate에 에러에 대한 메세지를 추가
            }
            return View(model);
        }


        /// <summary>
        /// 게시글 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        /* 게시글 수정인데... 어떻게 코딩해야 할지 모르겠다ㅠㅠ
        [HttpPost]
        public IActionResult Edit(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == UserNo)
            {
                //작성자가 아닌 상태
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('게시글 작성자만 수정할 수 있습니다.')", true);
                return RedirectToAction("Index", "Note");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDBContext())
                {
                    db.Notes.Add(model);
                    if (db.SaveChanges() > 0) // commit을 해 주는 것. _ 무슨개념이지..?
                    {
                        return Redirect("Index"); // = return RedirectToAction("Index", "Note");
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다."); //modelstate에 에러에 대한 메세지를 추가
            }
            return View(model);
        }*/

        /// <summary>
        /// 게시판 상세
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            //로그인이 안된 상태
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //로그인 된 상태
            using (var db = new AspnetNoteDBContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }
        }

        /// <summary>
        /// 게시글 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
