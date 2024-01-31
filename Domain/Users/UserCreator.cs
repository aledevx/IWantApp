using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IWantApp.Domain.Users;

public class UserCreator
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<(IdentityResult, string)> Create(string email, string password, List<Claim> claims)
    {
        var newUser = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
            return (result, String.Empty);

        return (await _userManager.AddClaimsAsync(newUser, claims), newUser.Id);
    }
}
