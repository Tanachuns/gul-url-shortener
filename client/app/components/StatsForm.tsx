import React from "react";

type Props = {
  submitHandler?: (event: React.FormEvent<HTMLFormElement>) => void;
};

export const StatsForm = (props: Props) => {
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
              Paste the Shortened URL to see stats.
            </legend>
            <input
              type="text"
              className="input input-sm"
              placeholder="Enter Shortened URL here"
              name="shortUrl"
            />
            </fieldset>
            <button className="btn btn-sm btn-secondary mt-3">View Stats</button>
        </form>
    </>
  );
};
