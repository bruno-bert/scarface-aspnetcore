const Loader = require('./Loader');
const TestProjectNameResolver = require("./TestProjectNameResolver");

/* Load classes from core */
const ControllerResolver = Loader.load('ResolverController');
const ModelResolver = Loader.load('ResolverModel');

/* Load classes from dialect plugin */
const CSharpDialect = Loader.load('CSharpDialect','scarface-dialect-csharp');

const config = {
 
  dialect: new CSharpDialect(),

  clearTargetFolder: true,

  //these are support files so will not be copied to the final target folder of the project
  //inform only the name of the file (without the paths)
  excludeFromCopy: [    
    "tplSchema.json",
    "Template.Config.js",
    "index.js",
    "TestProjectNameResolver.js",
    "NameResolver.js",
    "TemplateServerStub.cs",
    "TemplateController.cs",
    "TemplateModel.cs",
    "TemplateResource.cs",
    "AspNetCoreValidator.js",
    "AspNetCoreSchemaBuilder.js",
    "ProjectNameTest.csproj"
  ],

  files: [
    { name: "app/.vscode/launch.json", action: "copyTpl"},

    { name: "app/ProjectName.csproj", action: "rename" , targetPath: "app" },
    { name: "app/Startup.cs", action: "copyTpl" },
    { name: "app/Program.cs", action: "copyTpl" },
    { name: "app/appsettings.json", action: "copyTpl" },
    { name: "app/appsettings.Development.json", action: "copyTpl" },

    { name: "app/Infrastructure/ApiError.cs", action: "copyTpl" },
    { name: "app/Infrastructure/AppConfig.cs", action: "copyTpl" },
    { name: "app/Infrastructure/AppDBContext.cs", action: "copyTpl" },
    { name: "app/Infrastructure/AsyncEFRepository.cs", action: "copyTpl" },
    { name: "app/Infrastructure/IDesignTimeDbContextFactory.cs", action: "copyTpl" },
    
    {
      name: "app/Infrastructure/ConditionalRequireHttps.cs",
      action: "copyTpl"
    },
    { name: "app/Infrastructure/CustomExceptionFilter.cs", action: "copyTpl" },
    {
      name: "app/Infrastructure/CustomValidateModelAttribute.cs",
      action: "copyTpl"
    },
    { name: "app/Infrastructure/EFRepository.cs", action: "copyTpl" },
    { name: "app/Infrastructure/Extensions.cs", action: "copyTpl" },
    { name: "app/Infrastructure/Helper.cs", action: "copyTpl" },
    { name: "app/Infrastructure/IAsyncRepository.cs", action: "copyTpl" },
    { name: "app/Infrastructure/IRepository.cs", action: "copyTpl" },
    { name: "app/Infrastructure/MappingProfile.cs", action: "copyTpl" },
    { name: "app/Infrastructure/PaginationHeader.cs", action: "copyTpl" },
    { name: "app/Infrastructure/QueryParameters.cs", action: "copyTpl" },

    { name: "app/Controllers/AsyncRestController.cs", action: "copyTpl" },
    { name: "app/Controllers/IAsyncRest.cs", action: "copyTpl" },
    { name: "app/Controllers/IRest.cs", action: "copyTpl" },
    { name: "app/Controllers/RestController.cs", action: "copyTpl" },

    { name: "app/Models/GenericEntity.cs", action: "copyTpl" },
    { name: "app/Models/IEntity.cs", action: "copyTpl" },

    { name: "app/Resources/GenericResource.cs", action: "copyTpl" },
    { name: "app/Resources/IResource.cs", action: "copyTpl" },

    {
      name: "app/Controllers/TemplateController.cs",
      action: "custom",
      targetPath: "app/Controllers",
      stubFile: "app/Controllers/TemplateServerStub.cs",
      resolver: new ControllerResolver()
    },

    {
      name: "app/Models/TemplateModel.cs",
      action: "custom",
      targetPath: "app/Models",
      resolver: new ModelResolver()
    },

    {
      name: "app/Resources/TemplateResource.cs",
      action: "custom",
      targetPath: "app/Resources",
      resolver: new ModelResolver()
    },

    {
      name: "test/ProjectNameTest.csproj",
      action: "copyTpl",
      targetPath: "test",
      nameResolver: new TestProjectNameResolver()
    },
    { name: "test/RepositoryTests.cs", action: "copyTpl" },
    { name: "test/TestSetup.cs", action: "copyTpl" },
    { name: "test/UserRepositoryTests.cs", action: "copyTpl" },
    { name: "test/UserTestSetup.cs", action: "copyTpl" }
  ]
};

module.exports = config;
