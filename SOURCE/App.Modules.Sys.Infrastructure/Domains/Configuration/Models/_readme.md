## About ##

A common question that comes up is what is the difference between these Configuration objects
and those under App.XXX.Infrastructure.Services.Configuration. It's a good question.

These are *Messages* filled in by the ConfigurationObjectFactory from AppSettings, or other
sources. Being *Messages* they're sparse/anemic objects. They also are not smart enough to fill
themselves in. In a data import paradym (Raw Staging -> CleanedUp -> Normalized...) think of them
as barely to the right of Raw Staging input, in the fact that they are Typed.
These Messages are not not specific to any single Service (they often end up being used by just 
one, but still, they could be read by as many Services as need to refer to them).

But Service Configuration objects are smarter. They are injected with Services, which can read 
a Configuration Message object (the earlier mentioned kind), make decisions, etc. to make Properties
specific to the Service they are for.

So...Configuration messages are the output of HostingServices.
ServiceConfiguration objects are injected with HostingService. 
And Services are injected with ServiceConfiguration objects -- not the messages.

Services should not be having to be injected with HostingServices, and building up their configuraiton
settings -- just rely on injected configuration objects. 

Got that? Whew...