(function ()
{
    var myapp = angular.module('myapp');

    myapp.filter("myFilter", function ()
    {
        return function (input, searchText, AND_OR)
        {
            var returnArray = [],
            // Split on single or multi space
                splitext = searchText.toLowerCase().split(/\s+/),
            // Build Regexp with Logical AND using "look ahead assertions"
                regexp_and = "(?=.*" + splitext.join(")(?=.*") + ")",
            // Build Regexp with logicial OR
                regexp_or = searchText.toLowerCase().replace(/\s+/g, "|"),
            // Compile the regular expression
                re = new RegExp((AND_OR == "AND") ? regexp_and : regexp_or, "i");

            for (var x = 0; x < input.length; x++)
            {
                if (re.test(input[x]))
                {
                    returnArray.push(input[x]);
                }
            }
            return returnArray;
        }
    });

}());