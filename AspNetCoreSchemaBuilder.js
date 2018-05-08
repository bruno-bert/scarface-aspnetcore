const Loader = require('./Loader');
const SchemaBuilder = Loader.load('SchemaBuilder');

class AspNetCoreSchemaBuilder extends SchemaBuilder {

    constructor(options) {        
        super(options);
    }

    defaults() {
    
        if (this.theResolvedSchema.paths) {
            this.theResolvedSchema.paths.forEach(function(path) {
            path.responses = path.responses || [];
            path.parameters = path.parameters || [];
          });
        }

        return this;
    }

}

module.exports = AspNetCoreSchemaBuilder;