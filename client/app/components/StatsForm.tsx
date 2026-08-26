import React from "react";

type Props = {
  submitHandler?: (event: React.FormEvent<HTMLFormElement>) => void;
};

export const StatsForm = (props: Props) => {
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
