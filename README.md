# Data Dash
 
Data Dash is designed to minimise the impact of a common problem faced with expanding software systems, the need for more monitoring dashboards and the additional work required to maintain then.

Making sure code changes don't break your monitoring tools and keeping your monitoring tools update with systems changes to display correct data are time sinks that any large scale project will eventually encounter and this is where DataDash shines.

To reduce the chances of code changes breaking the monitoring tools and remove the need to update your monitoring tools separately from the product, DataDash calls a web api endpoint implemented in the product being monitored and requires the UI design be stored in code as part of the data transfer object being returned to the dashboard.

This means if a code change or merge would break the web api endpoint then it will throw a compile time error, letting the developer know that the code needs to be corrected. It also means the web api endpoint is calling the same logic as the application, so if the any methods called by the web api endpoint are updated it will also update the data returned to DataDash without having to update the monitoring tool separately.

Common scenarios where DataDash reduces development time are: database refactors, code restructuring, changing how a system works or code merges from multiple branches.
