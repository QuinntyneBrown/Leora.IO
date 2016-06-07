/**
 * Defines template and style encapsulation options available for Component's {@link View}.
 *
 * See {@link ViewMetadata#encapsulation}.
 */
export enum ViewEncapsulation {
    /**
     * Emulate `Native` scoping of styles by adding an attribute containing surrogate id to the Host
     * Element and pre-processing the style rules provided via
     * {@link ViewMetadata#styles} or {@link ViewMetadata#stylesUrls}, and adding the new Host Element
     * attribute to all selectors.
     *
     * This is the default option.
     */
    Emulated,
    /**
     * Use the native encapsulation mechanism of the renderer.
     *
     * For the DOM this means using [Shadow DOM](https://w3c.github.io/webcomponents/spec/shadow/) and
     * creating a ShadowRoot for Component's Host Element.
     */
    Native,
    /**
     * Don't provide any template or style encapsulation.
     */
    None
}