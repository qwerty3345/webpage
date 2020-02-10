using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStudy.Models
{
    public class User
    {
        /// <summary>
        /// 사용자 번호
        /// </summary>   -> xml주석, 변수 위에 마우스 올렸을 때 설명나옴
        [Key]   // PK 설정
        public int UserNo { get; set; }
        
        /// <summary>
        /// 사용자 이름
        /// </summary>
        [Required(ErrorMessage ="이름을 입력해주세요.")] // Not Null 설정
        public string UserName { get; set; }

        /// <summary>
        /// 사용자 ID
        /// </summary>
        [Required(ErrorMessage = "ID를 입력해주세요.")] // Not Null 설정
        public string UserId { get; set; }

        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        [Required(ErrorMessage = "비밀번호를 입력해주세요.")] // Not Null 설정
        public string UserPassword { get; set; }
     
    }
}
