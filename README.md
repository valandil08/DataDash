# Data Dash
 
Data Dash is designed to minimimise the impact of a common problem faced with expanding software systems, the need for more monitoring dashboards and the additional work required to maintain then.

By making the UI layout part of the data transfer object the dashboard receives from the api it calls, the data for the ui config and the data it is displaying is stored in code of the system being monitor and thus will throw error if a merge or refactor would break the code instead of finding out after deployment that the dashboard no longer works.

Common senarios that can cause this are are: database refactors, code restructuring, changing how a system works or large code merges from multiple branches.
All of these risk breaking the dashboards monitoring them if the code generating the data for the dashboard is external to the system being monitored.

