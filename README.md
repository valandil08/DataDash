# Data Dash
 
Data Dash is designed to minimimise the impact of a common problem faced with expanding software systems, the need for more monitoring dashboards and the additional work required to maintain then.

Making sure code changes don't break your monitoring tools and keeping your monitoring tools update with systems changes to display correct data are time sinks that any large scale project will eventually enounter and this is where DataDash shines.

To reduce the changes of code changes breaking the monitoring tools and remove the need to update your monitoring tools seperately from the product, DataDash calls a web api endpoint implemented in the product being monitored and requires the UI design be stored in code as part of the data transfer object being returned to the dashboard.

This means if a code change or merge would break the web api endpoint then it will throw a compile error, letting the developer know that the code needs to be corrected. It also means the web api endpoint is calling the same logic as the application, so if the any methods called by the web api enpoint are updated it will also update the data returned to DataDash without having to update the monitoring tool seperately.

Common senarios where DataDash reduces development time are: database refactors, code restructuring, changing how a system works or code merges from multiple branches.
All of these risk breaking the dashboards monitoring them if the code generating the data for the dashboard is external to the system being monitored and deployed seperately to the application.
