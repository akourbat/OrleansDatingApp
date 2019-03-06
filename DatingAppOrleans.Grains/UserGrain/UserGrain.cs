using DatingAppOrleans.Grains.DataAccess;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MediatR;
using System.Threading;
using DatingAppOrleans.Shared.GrainInterfaces;
using Stateless;
using DatingAppOrleans.Shared.DTOs;

namespace DatingAppOrleans.Grains.UserGrain
{
    public class UserGrain : Grain, IUserGrain
    {
        private readonly IMediator _mediator;
        private StateMachine<UserState, UserTriggers> _stateMachine;
        private UserState State => _stateMachine.State;

        public UserGrain(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnActivateAsync()
        {
            var state = await _mediator.Send(new GetUserState { UserName = this.GetPrimaryKeyString() });

            _stateMachine = new StateMachine<UserState, UserTriggers>(state);
            _stateMachine.OnUnhandledTrigger((s, trigger) => { });

            _stateMachine.Configure(UserState.New)
                .Permit(UserTriggers.Register, UserState.Registered);

            await base.OnActivateAsync();
        }

        public async Task<List<string>> GetUsers()
        {
            return await _mediator.Send(new RequestUsers { UserName = this.GetPrimaryKeyString(), State = this.State });
        }

        public async Task<UserDto> RegisterAsync(RegisterUserCommand command)
        {
            if (State != UserState.New) return null;
            var result = await _mediator.Send(command);
            _stateMachine.Fire(UserTriggers.Register);
            return result;
        }
    }


    public class GetUserState : IRequest<UserState>
    {
        public string UserName { get; set; }
    }

    public class GetUserStateHandler : IRequestHandler<GetUserState, UserState>
    {
        private readonly DatingContext context;

        public GetUserStateHandler(DatingContext context)
        {
            this.context = context;
        }

        public async Task<UserState> Handle(GetUserState request, CancellationToken cancellationToken)
        {
            var exists = await context.Users.AnyAsync(u => u.UserName == request.UserName);
            return exists ? UserState.Registered : UserState.New;
        }
    }




    public class RequestUsers : IRequest<List<string>>
    {
        public string UserName { get; set; }

        public UserState State { get; set; }
    }

    public class RequestUsersHandler : IRequestHandler<RequestUsers, List<string>>
    {
        private readonly DatingContext _context;

        public RequestUsersHandler(DatingContext context)
        {
            _context = context;
        }
        
        public async Task<List<string>> Handle(RequestUsers request, CancellationToken cancellationToken)
        {
            return await _context.Users.Select(u => u.UserName).ToListAsync();
        }
    }
}

