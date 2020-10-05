(function ($) {
    jQuery.validator.addMethod('requiredondecimalpropertyvalue',
    function (value, element, params) {
      var propertyValue = $('#' + params.nametocheck).val();
      if (propertyValue > 0 && value.length === 0 )
        return false;
      return true;
        });

    jQuery.validator.addMethod('requiredif',
        function (value, elemnet, params) {
            var propertyValue = $('#' + params.propertynametocheck).val();
            if (propertyValue.length === 0)
                return false;
            return true;
        });

    jQuery.validator.unobtrusive.adapters.add('requiredondecimalpropertyvalue',
    ['nametocheck'],
    function (options) {
        options.rules['requiredondecimalpropertyvalue'] = {
        nametocheck: options.params.nametocheck
      };
        options.messages['requiredondecimalpropertyvalue'] = options.message;
        });

    jQuery.validator.unobtrusive.adapters.add('requiredif',
        ['propertynametocheck'],
        function (options) {
            options.rules['requiredif'] = {
                propertynametocheck: options.params.propertynametocheck
            };
            options.messages['requiredif'] = options.message;
        });

})(jQuery);