using System;
using System.IO;
using Plugin.Settings;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace Xamarin.Aspects.Patterns.Data.Settings
{
    [PSerializable]
    public sealed class SettingsAttribute : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            var locationInfo = args.Location;
            if (locationInfo.LocationType == typeof(string))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (string) args.Value);
                args.ProceedSetValue();
                return;
            }

            if (locationInfo.LocationType == typeof(double))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (double)args.Value);
                args.ProceedSetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(Int32))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (Int32)args.Value);
                args.ProceedSetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(Int64))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (Int64)args.Value);
                args.ProceedSetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(bool))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (bool)args.Value);
                args.ProceedSetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(decimal))
            {
                CrossSettings.Current.AddOrUpdateValue(args.LocationFullName, (decimal)args.Value);
                args.ProceedSetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(DateTime))
            {
                CrossSettings.Current.GetValueOrDefault(args.LocationFullName, (DateTime)args.Value);
                args.ProceedSetValue();

                return;
            }

        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            var locationInfo = args.Location;
            if (locationInfo.LocationType == typeof(string))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, String.Empty);
                args.ProceedGetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(double))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, 0.0);
                args.ProceedGetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(Int32))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, 0);
                args.ProceedGetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(Int64))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, 0);
                args.ProceedGetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(bool))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, false);
                args.ProceedGetValue();

                return;
            }

            if (locationInfo.LocationType == typeof(decimal))
            {
                CrossSettings.Current.GetValueOrDefault(args.LocationFullName, new decimal(0));
                args.ProceedGetValue();
                return;
            }

            if (locationInfo.LocationType == typeof(DateTime))
            {
                args.Value = CrossSettings.Current.GetValueOrDefault(args.LocationFullName, new DateTime());
                args.ProceedGetValue();

                return;
            }
        }

        public override void RuntimeInitialize(LocationInfo locationInfo)
        {
            if (!CrossSettings.IsSupported)
                throw new DriveNotFoundException("Platform is not supported");
            base.RuntimeInitialize(locationInfo);
        }

        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            if (locationInfo.LocationType == typeof(string))
                return true;
            if (locationInfo.LocationType == typeof(double))
                return true;
            if (locationInfo.LocationType == typeof(Int32))
                return true;
            if (locationInfo.LocationType == typeof(Int64))
                return true;
            if (locationInfo.LocationType == typeof(bool))
                return true;
            if (locationInfo.LocationType == typeof(decimal))
                return true;
            if (locationInfo.LocationType == typeof(DateTime))
                return true;

            Message.Write(locationInfo, SeverityType.Error, "SettingsAspects",
                "Settings cannot be applied on property with type {0} " +
                "please check https://jamesmontemagno.github.io/SettingsPlugin/GettingStarted.html " +
                "for more informations regarding supported types",
                locationInfo.LocationType.FullName
            );
            return false;

        }
    }
}
