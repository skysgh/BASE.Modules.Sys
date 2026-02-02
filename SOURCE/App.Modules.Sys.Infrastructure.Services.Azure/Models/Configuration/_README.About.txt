These are Configuration objects
for the Azure Cloud Services implementations
of this system's Infrastructure Services.

Configuration Objects
==================================
* Although Azure App Configuration has some good design points, to remain 
  portable, the application does not use Azure App Configuration directly,
  and remains a little bit old-school, using Configuration Objects.
* Being Configuration Objects, they are hydrated via Services that internally
  read values from the host AppSettings, Azure App Configuration, or a Keystore.
* Each property is decorated with Attributes specifying Aliases to Property Names
  (to make it easier to setup), as well as Hints as to where its appropriate/acceeptable
  to look for values.
* Configuration objects are hydrated by services at first use, and 
  then should be cached for a duration, as hydrating them can be expensive.


The Root (or Prefix) Resource Name
==================================
* In general Azure Resources are generally given the same lowercase/short resource name.
* Even though many resources have the same name, they are distinguishable from each other by the Type,
* You can't get mixed up as they each have their own specific APIs.
* This default name is defined in the basic AzureCommonConfigurationSettings object.
* That said, when there are two Resources of the same type, you can't use the same name for both.
* In which case the 'default' resource name is used as a 'root' or 'prefix', and appended with a short suffix (eg: 'corpappbranchenv'+ 'sqldb1' and 'corpappbranchenv' + 'sqldb2')