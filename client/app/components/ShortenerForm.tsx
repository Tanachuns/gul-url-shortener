import React from "react";

type Props = {
  submitHandler?: (event: React.FormEvent<HTMLFormElement>) => void;
};

export const ShortenerForm = (props: Props) => {
  const [customToggles, setCustomToggles] = React.useState({
    customAliasToggle: false,
    androidUrlToggle: false,
    iosUrlToggle: false
  });

  const handleToggleChange = (event: React.ChangeEvent<HTMLInputElement, HTMLInputElement>) => {
    console.log(event.target.name, event.target.checked);
    setCustomToggles({ ...customToggles, [event.target.name]: event.target.checked })
  }

  return (
    <>
        <form className="centered-form" onSubmit={props.submitHandler}>
          <fieldset className="fieldset">
            <legend className="fieldset-legend">
              Paste the URL to be shortened
            </legend>
            <input
              type="text"
              className="input input-sm"
              placeholder="Enter URL here"
              name="Longurl"
            />
            <p className="label">
              URL shortener allows to create a shortened link making it easy to
              share
            </p>
          </fieldset>
          <div>
            <label className="label text-sm">
              <input
                type="checkbox"
                className="toggle toggle-xs"
                checked={customToggles.customAliasToggle}
                onChange={(e) => handleToggleChange(e)}
                name="customAliasToggle"
              />
              Custom Alias
            </label>
           {customToggles.customAliasToggle &&  <fieldset className="fieldset">
              <input
                type="text"
                className="input input-sm"
                placeholder="Enter custom alias here"
                name="customAlias"
              />
            </fieldset>}
          </div>
<div>
            <label className="label text-sm">
              <input
                type="checkbox"
                name="androidUrlToggle"
                checked={customToggles.androidUrlToggle}
                onChange={(e) => handleToggleChange(e)}
                className="toggle toggle-xs"
              />
              Android Specific Url
            </label>
              {customToggles.androidUrlToggle &&   <fieldset className="fieldset">
              <input
                type="text"
                className="input input-sm"
                placeholder="Enter Android specific URL here"
                name="customAlias"
                />
            </fieldset>
          }
           
          </div>

           <div>
            <label className="label text-sm">
              <input
                type="checkbox"
                name="iosUrlToggle"
                checked={customToggles.iosUrlToggle}
                onChange={(e) => handleToggleChange(e)}
                className="toggle toggle-xs"
              />
              iOS Specific Url
            </label>
           {customToggles.iosUrlToggle &&   <fieldset className="fieldset">
              <input
                type="text"
                className="input input-sm"
                placeholder="Enter iOS specific URL here"
                name="iosUrl"
              />
            </fieldset>
          }
          </div>
            <button className="btn btn-sm btn-primary mt-3">Shorten URL</button>
            <a className="btn btn-sm btn-link btn-secondary ml-3 mt-3" href="/stats">View Stats</a>
        </form>
    </>
  );
};
