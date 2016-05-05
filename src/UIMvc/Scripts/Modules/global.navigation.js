(function ($) {
    $(function() {
        $("input.toggle[type='checkbox']").bootstrapSwitch();

        $("#cancel").bind("click", function () {
            window.history.back();
        });

        $("#continue").on("click", function (e) {
            $("#return_url").val($("#continue").data("url"));
        });
    });
})(window.$);