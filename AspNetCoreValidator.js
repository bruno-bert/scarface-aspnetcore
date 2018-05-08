const Loader = require('./Loader');
const Configurator = require("./Template.Config.js");
const SchemaValidator = Loader.load('SchemaValidator');

class AspNetCoreValidator extends SchemaValidator {


    constructor() {
        super();
        this.MESSAGE_INVALID_SCHEMA = "Invalid Schema";
        this.MESSAGE_NOTFOUND_TAG_VERB = "Tag verb is required for all paths";
        this.MESSAGE_NOTFOUND_TAG_VERB = "Tag responses is required for all paths";
    }

   

    _validateTemplateSchema(schema) {

        if (schema.paths) {

            if (!schema.verb)
                return this._fail(this.MESSAGE_NOTFOUND_TAG_VERB);

            if (!schema.responses)
                return this._fail(this.MESSAGE_NOTFOUND_TAG_RESPONSES);

        }

        return this._success();
    }


}

module.exports = AspNetCoreValidator;