const Loader = require('./Loader');
const Resolver = Loader.load('Resolver');

class TestProjectNameResolver extends Resolver {
    
    resolveName( options = { fileName: null, extension : '.cs' } ){
        let name = super.resolveName(  options ); 
        let prefix = "Test";
        return prefix.concat(name);
    }    
}

module.exports = TestProjectNameResolver;
