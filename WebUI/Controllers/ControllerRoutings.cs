using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace WebUI.Controllers
{
    public static class ControllerRoutings
    {
        public const string ApiPrefix = "api/";

        public static void MapAppRoutes(this IRouteBuilder router)
        {
            router.MapRoute("getAllPosts", $"{ApiPrefix}posts", new { controller = "Posts", action = "Get" });
            router.MapRoute("createPost", $"{ApiPrefix}posts", new { controller = "Posts", action = "Create" });
            router.MapRoute("getPostById", $"{ApiPrefix}posts/{{id}}", new { controller = "Posts", action = "GetById" });
            router.MapRoute("updatePost", $"{ApiPrefix}posts/{{id}}", new { controller = "Posts", action = "Update" });
            router.MapRoute("deletePost", $"{ApiPrefix}posts/{{id}}", new { controller = "Posts", action = "Delete" });
            router.MapRoute("getAllPostsByUser", $"{ApiPrefix}users/{{id}}/posts", new { controller = "Posts", action = "GetByUser" });

            router.MapRoute("getAllComments", $"{ApiPrefix}comments", new { controller = "Comments", action = "Get" });
            router.MapRoute("createComment", $"{ApiPrefix}comments", new { controller = "Comments", action = "Create" });
            router.MapRoute("getCommentById", $"{ApiPrefix}comments/{{id}}", new { controller = "Comments", action = "GetById" });
            router.MapRoute("updateComment", $"{ApiPrefix}comments/{{id}}", new { controller = "Comments", action = "Update" });
            router.MapRoute("deleteComment", $"{ApiPrefix}comments/{{id}}", new { controller = "Comments", action = "Delete" });
            router.MapRoute("getAllCommentByPost", $"{ApiPrefix}posts/{{id}}/comments", new { controller = "Comments", action = "GetByPost" });

            router.MapRoute("getAllUsers", $"{ApiPrefix}users", new {controller = "Users", action = "Get"});
            router.MapRoute("signUp", $"{ApiPrefix}signUp", new { controller = "Users", action = "SignUp" });
            router.MapRoute("signIn", $"{ApiPrefix}signIn", new { controller = "Users", action = "SignIn" });
            router.MapRoute("getUserById", $"{ApiPrefix}users/{{id}}", new { controller = "Users", action = "GetById" });
            router.MapRoute("updateUser", $"{ApiPrefix}users/{{id}}", new { controller = "Users", action = "Update" });
            router.MapRoute("deleteUser", $"{ApiPrefix}users/{{id}}", new { controller = "Users", action = "Delete" });

            router.MapSpaFallbackRoute(name: "spa-fallback-api", templatePrefix: ApiPrefix, defaults: new { controller = "Main", action = "ApiNotFound" });
            router.MapSpaFallbackRoute(name: "spa-fallback", defaults: new { controller = "Main", action = "GetAngularPage" });
        }

    }
}
