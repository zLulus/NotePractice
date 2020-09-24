module.exports = function (callback, name) {

    var greet = function (name) {
        return "Hello " + name;
    }

    callback(null, greet(name));
}