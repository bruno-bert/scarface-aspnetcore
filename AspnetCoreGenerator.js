const Loader = require('./Loader');
const Configurator = require("./Template.Config.js");
const AspNetCoreValidator = require('./AspNetCoreValidator');
const AspNetCoreSchemaBuilder = require('./AspNetCoreSchemaBuilder');
const ProjectGenerator = Loader.load('ProjectGenerator');

class AspnetCoreGenerator extends ProjectGenerator { 

    
    constructor(params){
        
        super(params);

        
        this.theConfigurator = Configurator;  

        this.theSchemaBuilder = new AspNetCoreSchemaBuilder({
            validator: new AspNetCoreValidator(),
            dialect: Configurator.dialect,
            template: this.template
        }); 

    }

}

module.exports = AspnetCoreGenerator;


