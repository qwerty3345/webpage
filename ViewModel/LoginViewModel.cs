using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.ViewModel
{
    public class LoginViewModel  //로그인 할 때만 사용 할 모델, 뷰모델
    {
        [Required(ErrorMessage ="사용자 ID를 입력하세요")]
        public string UserId { get; set; }

        [Required(ErrorMessage ="사용자 비밀번호를 입력하세요.")]
        public string UserPassword { get; set; }
    }
}
