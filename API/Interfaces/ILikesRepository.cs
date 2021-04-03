using API.DTOs;
using API.Entities;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUSerId, int likedUserid);
        Task<AppUser> GetUserWithLIkes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesparams);
    }
}
