using System;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public IUserRepository userRepository { get; set; }
    public ICommentRepository commentRepository { get; set; }
    public IPostRepository postRepository { get; set; }

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    internal async Task StartAsync()
    {
        throw new NotImplementedException();
    }

    
}
