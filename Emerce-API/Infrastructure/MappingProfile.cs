using AutoMapper;
namespace Emerce_API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /* USER MAPS */
            //user create 
            CreateMap<Emerce_DB.Entities.User, Emerce_Model.User.UserCreateModel>();
            CreateMap<Emerce_Model.User.UserCreateModel, Emerce_DB.Entities.User>();
            //user login
            CreateMap<Emerce_DB.Entities.User, Emerce_Model.User.UserLoginModel>();
            CreateMap<Emerce_Model.User.UserLoginModel, Emerce_DB.Entities.User>();
            //CreateMap<Emerce_Model.User.UserViewModel, Emerce_Model.User.UserLoginModel>();
            //CreateMap<Emerce_Model.User.UserLoginModel, Emerce_Model.User.UserViewModel>();
            //user view
            CreateMap<Emerce_Model.User.UserViewModel, Emerce_DB.Entities.User>();
            CreateMap<Emerce_DB.Entities.User, Emerce_Model.User.UserViewModel>();
            //.ForMember(dest => dest.Iuser, opt => opt.MapFrom(src => src.Email));
            //.ForMember(dest => dest.Iuser, opt => opt.MapFrom(src => src. IuserNavigation.Email));


            /* PRODUCT MAPS */
            //product create
            CreateMap<Emerce_Model.Product.ProductCreateModel, Emerce_DB.Entities.Product>();
            CreateMap<Emerce_DB.Entities.Product, Emerce_Model.Product.ProductCreateModel>();
            //product view
            CreateMap<Emerce_Model.Product.ProductViewModel, Emerce_DB.Entities.Product>();
            CreateMap<Emerce_DB.Entities.Product, Emerce_Model.Product.ProductViewModel>()
                .ForMember(dest => dest.Iuser, opt => opt.MapFrom(src => src.IuserNavigation.Email))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            /* CATEGORY MAPS */
            //category create
            CreateMap<Emerce_Model.Category.CategoryCreateModel, Emerce_DB.Entities.Category>();
            CreateMap<Emerce_DB.Entities.Category, Emerce_Model.Category.CategoryCreateModel>();
            //category view
            CreateMap<Emerce_Model.Category.CategoryViewModel, Emerce_DB.Entities.Category>();
            CreateMap<Emerce_DB.Entities.Category, Emerce_Model.Category.CategoryViewModel>()
                .ForMember(dest => dest.Iuser, opt => opt.MapFrom(src => src.IuserNavigation.Email));
        }
        //showing detail on list is not required so I will use this map for get detail 
        //CreateMap<Emerce_DB.Entities.Category, Emerce_Model.Category.CategoryViewModel>().ForMember(dest => dest.Iuser, opt => opt.MapFrom(src => src.IuserNavigation.Email));            
    }
}
