
function somarFloat(val1, val2) {
    var result = 0;
    result = parseFloat(val1) + parseFloat(val2);

    var moeda = numeroParaMoeda(result);

    return moeda;
}

function moedaParaNumero(valor) {
    return isNaN(valor) == false ? parseFloat(valor) : parseFloat(valor.replace("R$", "").replace(".", "").replace(",", "."));
}

function numeroParaMoeda(n, c, d, t) {

    c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}

$("label.error").hide();

function validateError(id, msg, isShow) {
    if (isShow) {
        $(id).addClass("error");
        $(id + "-error").text(msg);
        $(id + "-error").show();
    } else {
        $(id).removeClass("error");
        $(id + "-error").text(msg);
        $(id + "-error").hide();
    }
}

function converteMoedaFloat(valor) {

    if (valor === "") {
        valor = 0;
    } else {
        valor = valor.replace(".", "");
        valor = valor.replace(",", ".");
        valor = parseFloat(valor);
    }
    return valor;
}


function initializeJS() {

    //tool tips
    jQuery('.tooltips').tooltip();

    //popovers
    jQuery('.popovers').popover();

    //custom scrollbar
        //for html
    //jQuery("html").niceScroll({styler:"fb",cursorcolor:"#007AFF", cursorwidth: '6', cursorborderradius: '10px', background: '#F7F7F7', cursorborder: '', zindex: '1000'});
        //for sidebar
    jQuery("#sidebar").niceScroll({styler:"fb",cursorcolor:"#C90000", cursorwidth: '3', cursorborderradius: '10px', background: '#F7F7F7', cursorborder: ''});
        // for scroll panel
    //jQuery(".scroll-panel").niceScroll({styler:"fb",cursorcolor:"#007AFF", cursorwidth: '3', cursorborderradius: '10px', background: '#F7F7F7', cursorborder: ''});
    
    //sidebar dropdown menu
    jQuery('#sidebar .sub-menu > a').click(function () {
        var last = jQuery('.sub-menu.open', jQuery('#sidebar'));        
        jQuery('.menu-arrow').removeClass('arrow_carrot-right');
        jQuery('.sub', last).slideUp(200);
        var sub = jQuery(this).next();
        if (sub.is(":visible")) {
            jQuery('.menu-arrow').addClass('arrow_carrot-right');            
            sub.slideUp(200);
        } else {
            jQuery('.menu-arrow').addClass('arrow_carrot-down');            
            sub.slideDown(200);
        }
        var o = (jQuery(this).offset());
        diff = 200 - o.top;
        if(diff>0)
            jQuery("#sidebar").scrollTo("-="+Math.abs(diff),500);
        else
            jQuery("#sidebar").scrollTo("+="+Math.abs(diff),500);
    });

    // sidebar menu toggle
    jQuery(function() {
        function responsiveView() {
            var wSize = jQuery(window).width();
            if (wSize <= 768) {
                jQuery('#container').addClass('sidebar-close');
                jQuery('#sidebar > ul').hide();
            }

            if (wSize > 768) {
                jQuery('#container').removeClass('sidebar-close');
                jQuery('#sidebar > ul').show();
            }
        }
        jQuery(window).on('load', responsiveView);
        jQuery(window).on('resize', responsiveView);
    });

    jQuery('.toggle-nav').click(function () {
        if (jQuery('#sidebar > ul').is(":visible") === true) {
            jQuery('#main-content').css({
                'margin-left': '0px'
            });
            jQuery('#sidebar').css({
                'margin-left': '-180px'
            });
            jQuery('#sidebar > ul').hide();
            jQuery("#container").addClass("sidebar-closed");
        } else {
            jQuery('#main-content').css({
                'margin-left': '180px'
            });
            jQuery('#sidebar > ul').show();
            jQuery('#sidebar').css({
                'margin-left': '0'
            });
            jQuery("#container").removeClass("sidebar-closed");
        }
    });

    //bar chart
    if (jQuery(".custom-custom-bar-chart")) {
        jQuery(".bar").each(function () {
            var i = jQuery(this).find(".value").html();
            jQuery(this).find(".value").html("");
            jQuery(this).find(".value").animate({
                height: i
            }, 2000)
        })
    }

}

jQuery(document).ready(function(){
    initializeJS();
});



/*Search dropdown*/
(function ($) {

    $.fn.searchit = function (options) {

        return this.each(function () {

            $.fn.searchit.globals = $.fn.searchit.globals || {
                counter: 0
            }
            $.fn.searchit.globals.counter++;
            var $counter = $.fn.searchit.globals.counter;

            var $t = $(this);
            var opts = $.extend({}, $.fn.searchit.defaults, options);

            // Setup default text field and class
            if (opts.textField == null) {
                $t.before("<input type='textbox' id='__searchit" + $counter + "'><br>");
                opts.textField = $('#__searchit' + $counter);
            }
            if (opts.textField.length > 1) opts.textField = $(opts.textField[0]);

            if (opts.textFieldClass) opts.textField.addClass(opts.textFieldClass);
            //MY CODE-------------------------------------------------------------------
            if (opts.selected) opts.textField.val($(this).find(":selected").val());
            //MY CODE ENDS HERE -------------------------------------------------------
            if (opts.dropDown) {
                $t.css("padding", "5px")
                    .css("margin", "-5px -20px -5px -5px");

                $t.wrap("<div id='__searchitWrapper" + $counter + "' />");
                opts.wrp = $('#__searchitWrapper' + $counter);
                opts.wrp.css("display", "inline-block")
                    .css("vertical-align", "top")
                    .css("overflow", "hidden")
                    .css("border", "solid grey 1px")
                    .css("position", "relative")
                    .hide();
                if (opts.dropDownClass) opts.wrp.addClass(opts.dropDownClass);
            }

            opts.optionsFiltered = [];
            opts.optionsCache = [];

            // Save listbox current content
            $t.find("option").each(function (index) {
                opts.optionsCache.push(this);
            });

            // Save options 
            $t.data('opts', opts);

            // Hook listbox click
            $t.click(function (event) {
                _opts($t).textField.val($(this).find(":selected").text());
                _opts($t).wrp.hide();
                event.stopPropagation();
            });

            // Hook html page click to close dropdown
            $("html").click(function () {
                _opts($t).wrp.hide();
            });

            // Hook the keyboard and we're done
            _opts($t).textField.keyup(function (event) {
                if (event.keyCode == 13) {
                    $(this).val($t.find(":selected").text());
                    _opts($t).wrp.hide();
                    return;
                }
                setTimeout(_findElementsInListBox($t, $(this)), 50);
            })

        })


        function _findElementsInListBox(lb, txt) {

            if (!lb.is(":visible")) {
                _showlb(lb);
            }

            _opts(lb).optionsFiltered = [];
            var count = _opts(lb).optionsCache.length;
            var dropDown = _opts(lb).dropDown;
            var searchText = txt.val().toLowerCase();

            // find match (just the old classic loop, will make the regexp later)
            $.each(_opts(lb).optionsCache, function (index, value) {
                if ($(value).text().toLowerCase().indexOf(searchText) > -1) {
                    // save matching items 
                    _opts(lb).optionsFiltered.push(value);
                }

                // Trigger a listbox reload at the end of cycle    
                if (!--count) {
                    _filterListBox(lb);
                }
            });
        }

        function _opts(lb) {
            return lb.data('opts');
        }

        function _showlb(lb) {
            if (_opts(lb).dropDown) {
                var tf = _opts(lb).textField;
                lb.attr("size", _opts(lb).size);
                _opts(lb).wrp.show().offset({
                    top: tf.offset().top + tf.outerHeight(),
                    left: tf.offset().left
                });
                _opts(lb).wrp.css("width", tf.outerWidth() + "px");
                lb.css("width", (tf.outerWidth() + 25) + "px");
            }
        }

        function _filterListBox(lb) {
            lb.empty();

            if (_opts(lb).optionsFiltered.length == 0) {
                lb.append("<option>" + _opts(lb).noElementText + "</option>");
            } else {
                $.each(_opts(lb).optionsFiltered, function (index, value) {
                    lb.append(value);
                });
                lb[0].selectedIndex = 0;
            }
        }
    }

    $.fn.searchit.defaults = {
        textField: null,
        textFieldClass: null,
        dropDown: true,
        dropDownClass: null,
        size: 10,
        filtered: true,
        noElementText: "Nenhum registro encontrado!",
        //MY CODE------------------------------------------
        selected: false
        //MY CODE ENDS ------------------------------------
    }

}(jQuery))