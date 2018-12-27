using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CStore.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Set to false to not generate bundles in release mode
            //BundleTable.EnableOptimizations = false;

            //This will replace all relative paths (within the CSS files) with their absolute-pathed counterpart.
            //This means that no matter what you call the bundle, the resources will still be resolved.
            IItemTransform cssFixer = new CssRewriteUrlTransform();

            // Catalyst
            bundles.Add(new ScriptBundle("~/bundles/scripts/catalyst").Include(
                      "~/Scripts/Catalyst/Catalyst.js",
                      "~/Scripts/Catalyst/UI/Validation.js",
                      "~/Scripts/Catalyst/UI/Message.js",
                      "~/Scripts/Catalyst/UI/ToastMessage.js",
                      "~/Scripts/Catalyst/UI/Form.js",
                      "~/Scripts/Catalyst/UI/Formatting.js",
                      "~/Scripts/Catalyst/UI/Menu.js",
                      "~/Scripts/Catalyst/UI/DatePicker.js",
                      "~/Scripts/Catalyst/UI/NumericField.js",
                      "~/Scripts/Catalyst/UI/MaskedField.js",
                      "~/Scripts/Catalyst/UI/Modal.js",
                      "~/Scripts/Catalyst/UI/FormState.js",
                      "~/Scripts/Catalyst/UI/TimeZone.js",
                      "~/Scripts/Catalyst/UI/Spinner.js"));

            // Catalyt CSS styles
            bundles.Add(new StyleBundle("~/bundles/css/catalystStyles").Include(
                      "~/css/Catalyst/UI/Spinner.css"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/bundles/css/bootstrapInspinia")
                            //Be sure to use the minified version. If you dont, the minified version will be sent
                            //back in the bundle and NOT run through the cssfixer. By specifying the minified version, it
                            //forces it to be run through the css fixer.
                            .Include("~/Content/bootstrap.min.css", cssFixer)
                            .Include("~/css/animate.css", cssFixer)
                            .Include("~/css/style.css", cssFixer));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/bundles/css/main")
                            .Include("~/css/main.css", cssFixer));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/bundles/css/fonts").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", cssFixer));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/scripts/jquery").Include(
                        "~/Scripts/jquery-2.2.2.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // jQueryUI CSS
            bundles.Add(new StyleBundle("~/bundles/css/jqueryuiStyles")
                            //Be sure to use the minified version. If you dont, the minified version will be sent
                            //back in the bundle and NOT run through the cssfixer. By specifying the minified version, it
                            //forces it to be run through the css fixer.
                            .Include("~/Scripts/plugins/jquery-ui/jquery-ui.css", cssFixer));

            // jQueryUI
            bundles.Add(new ScriptBundle("~/bundles/scripts/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.js"));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/scripts/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/scripts/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // moment
            bundles.Add(new ScriptBundle("~/plugins/scripts/moment").Include(
                      "~/Scripts/plugins/moment/moment.js"));

            //select2 - used for better drop downs
            bundles.Add(new ScriptBundle("~/plugins/scripts/select2").Include(
                       "~/Scripts/plugins/select2/select2.full.js"));

            bundles.Add(new StyleBundle("~/plugins/css/select2")
                .Include("~/css/plugins/select2/select2.css", cssFixer));

            bundles.Add(new StyleBundle("~/plugins/css/select2-bootstrap")
                .Include("~/css/plugins/select2/select2-bootstrap.css", cssFixer));

            //js-cookie - used for manipulating cookies
            bundles.Add(new ScriptBundle("~/plugins/scripts/jsCookie").Include(
                      "~/Scripts/plugins/js-cookie/js.cookie.js"));

            //jsTimezoneDetect - used to detect time zones
            bundles.Add(new ScriptBundle("~/plugins/scripts/jsTimezoneDetect").Include(
                      "~/Scripts/plugins/jsTimezoneDetect/jstz.js"));


            // Bootstrap DateTime Picker css styles
            bundles.Add(new StyleBundle("~/plugins/css/bootstrap-datetimepickerStyles").Include(
                      "~/css/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css"));

            // Bootstrap DateTime Picker
            bundles.Add(new ScriptBundle("~/plugins/scripts/bootstrap-datetimepicker").Include(
                      "~/Scripts/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js"));

            // datePickerStyles styles
            bundles.Add(new StyleBundle("~/plugins/css/bootstrap-datepickerStyles").Include(
                      "~/css/plugins/bootstrap-datepicker/datepicker3.css"));

            // bootstrap-datepicker
            bundles.Add(new ScriptBundle("~/plugins/scripts/bootstrap-datepicker").Include(
                      "~/Scripts/plugins/bootstrap-datepicker/bootstrap-datepicker.js"));

            // HTML 5 Placeholder Shim to make placeholder text work in IE9 and 10
            bundles.Add(new ScriptBundle("~/plugins/scripts/html5-placeholder-shim").Include(
                      "~/Scripts/plugins/html5-placeholder-shim/html5-placeholder-shim.js"));

            // validate
            bundles.Add(new ScriptBundle("~/bundles/scripts/validate").Include(
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.unobtrusive.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/scripts/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.js",
                      "~/Scripts/app/inspinia.js",
                      "~/Scripts/plugins/pace/pace.min.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/scripts/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/scripts/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/scripts/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/scripts/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/plugins/css/blueimp-gallery").Include(
                      "~/css/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/scripts/lightboxGallery").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/scripts/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/css/morrisStyles").Include(
                      "~/css/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/scripts/morris").Include(
                      "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/scripts/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/scripts/rickshaw").Include(
                      "~/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/scripts/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/plugins/css/iCheckStyles").Include(
                      "~/css/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/scripts/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/plugins/css/dataTablesStyles").Include(
                      //Dont need the datatables css if you are using bootstrap
                      //"~/css/plugins/dataTables/jquery.dataTables.min.css",
                      //"~/css/plugins/dataTables/jquery.dataTables_themeroller.css",
                      "~/css/plugins/dataTables/dataTables.bootstrap.css",
                      "~/css/plugins/dataTables/responsive.bootstrap.css",
                      "~/css/plugins/dataTables/select.bootstrap.css",
                      "~/css/plugins/dataTables/buttons.bootstrap.css"
                      ));


            // dataTables
            bundles.Add(new ScriptBundle("~/plugins/scripts/dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/plugins/dataTables/dataTables.buttons.js",
                      "~/Scripts/plugins/dataTables/buttons.bootstrap.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/plugins/dataTables/dataTables.select.js"
                      ));

            // jeditable
            bundles.Add(new ScriptBundle("~/plugins/scripts/jeditable").Include(
                      "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/css/codeEditorStyles").Include(
                      "~/css/plugins/codemirror/codemirror.css",
                      "~/css/plugins/codemirror/ambiance.css"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/scripts/codeEditor").Include(
                      "~/Scripts/plugins/codemirror/codemirror.js",
                      "~/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/scripts/nestable").Include(
                      "~/Scripts/plugins/nestable/jquery.nestable.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/css/fullCalendarStyles").Include(
                      "~/css/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar
            bundles.Add(new ScriptBundle("~/plugins/scripts/fullCalendar").Include(
                      //"~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/moment/moment.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.js"));

            // vectorMap
            bundles.Add(new ScriptBundle("~/plugins/scripts/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/plugins/css/ionRangeStyles").Include(
                      "~/css/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange
            bundles.Add(new ScriptBundle("~/plugins/scripts/ionRange").Include(
                      "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/css/nouiSliderStyles").Include(
                      "~/css/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider
            bundles.Add(new ScriptBundle("~/plugins/scripts/nouiSlider").Include(
                      "~/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/css/jasnyBootstrapStyles").Include(
                      "~/css/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap
            bundles.Add(new ScriptBundle("~/plugins/scripts/jasnyBootstrap").Include(
                      "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/css/switcheryStyles").Include(
                      "~/css/plugins/switchery/switchery.css"));

            // switchery
            bundles.Add(new ScriptBundle("~/plugins/scripts/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/plugins/css/chosenStyles").Include(
                      "~/css/plugins/chosen/chosen.css"));

            // chosen
            bundles.Add(new ScriptBundle("~/plugins/scripts/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob
            bundles.Add(new ScriptBundle("~/plugins/scripts/knob").Include(
                      "~/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/css/wizardStepsStyles").Include(
                      "~/css/plugins/steps/jquery.steps.css"));

            // wizardSteps
            bundles.Add(new ScriptBundle("~/plugins/scripts/wizardSteps").Include(
                      "~/Scripts/plugins/steps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/plugins/css/dropZoneStyles").Include(
                      "~/css/plugins/dropzone/basic.css",
                      "~/css/plugins/dropzone/dropzone.css"));

            // dropZone
            bundles.Add(new ScriptBundle("~/plugins/scripts/dropZone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/css/summernoteStyles").Include(
                      "~/css/plugins/summernote/summernote.css",
                      "~/css/plugins/summernote/summernote-bs3.css"));

            // summernote
            bundles.Add(new ScriptBundle("~/plugins/scripts/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification
            bundles.Add(new ScriptBundle("~/plugins/scripts/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/css/toastrStyles").Include(
                      "~/css/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/scripts/colorpicker").Include(
                      "~/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/plugins/css/colorpickerStyles").Include(
                      "~/css/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/scripts/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/css/imagecropperStyles").Include(
                      "~/css/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/scripts/jsTree").Include(
                      "~/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/plugins/css/jsTree")
                    .Include("~/css/plugins/jsTree/style.min.css", cssFixer));

            // Diff
            bundles.Add(new ScriptBundle("~/plugins/scripts/diff").Include(
                      "~/Scripts/plugins/diff_match_patch/javascript/diff_match_patch.js",
                      "~/Scripts/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"));

            // Idle timer
            bundles.Add(new ScriptBundle("~/plugins/scripts/idletimer").Include(
                      "~/Scripts/plugins/idle-timer/idle-timer.min.js"));

            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/scripts/tinycon").Include(
                      "~/Scripts/plugins/tinycon/tinycon.min.js"));

            // autoNumeric
            bundles.Add(new ScriptBundle("~/plugins/scripts/autoNumeric").Include(
                      "~/Scripts/plugins/autoNumeric/autoNumeric.js"));

            // jquery.maskedinput
            bundles.Add(new ScriptBundle("~/plugins/scripts/jquery.maskedinput").Include(
                      "~/Scripts/plugins/jquery.maskedinput/jquery.maskedinput.js"));
        }
    }
}